#!/usr/bin/env python3
"""
FutureVest - Seed Generator (World Bank) - Robust Version

Objetivo:
- Descargar indicadores macroeconómicos reales (World Bank API)
- Filtrar años 2020–2026
- Generar CSV: tools/seed/output/seed_indicators.csv

Mejoras vs versión simple:
- Reintentos (retry) con backoff ante timeouts
- Paginación automática
- Timeout mayor por request
- Manejo de errores por país/indicador sin tumbar todo el proceso
"""

from __future__ import annotations

import csv
import json
import sys
import time
import urllib.parse
import urllib.request
import socket
from dataclasses import dataclass
from pathlib import Path
from typing import Dict, List, Optional, Tuple

# -----------------------------
# Config general
# -----------------------------
START_YEAR = 2020
END_YEAR = 2026  # es posible que 2026 no tenga data aún (se exporta vacío)
OUTPUT_DIR = Path(__file__).resolve().parent / "output"
OUTPUT_CSV = OUTPUT_DIR / "seed_indicators.csv"

WB_API_BASE = "https://api.worldbank.org/v2"

# HTTP settings
REQUEST_TIMEOUT_SECONDS = 60          # subimos el timeout
MAX_RETRIES = 5                       # reintentos
RETRY_BACKOFF_BASE_SECONDS = 1.2      # backoff incremental

# Países (ISO3)
COUNTRIES = {
    "USA": "United States",
    "SGP": "Singapore",
    "ARE": "United Arab Emirates",
    "IRL": "Ireland",
    "MEX": "Mexico",
    "KOR": "Korea, Rep.",
    "DOM": "Dominican Republic",
    "CUB": "Cuba",
    "VEN": "Venezuela, RB",
}

# Indicadores World Bank (códigos oficiales)
INDICATORS = {
    "GDP_GROWTH_PCT": ("NY.GDP.MKTP.KD.ZG", "GDP growth (annual %)"),
    "INFLATION_PCT": ("FP.CPI.TOTL.ZG", "Inflation, consumer prices (annual %)"),
    "UNEMPLOYMENT_PCT": ("SL.UEM.TOTL.ZS", "Unemployment, total (% of labor force)"),
    "REAL_INTEREST_PCT": ("FR.INR.RINR", "Real interest rate (%)"),
}


@dataclass
class DataPoint:
    year: int
    value: Optional[float]


def _sleep_backoff(attempt: int) -> None:
    # attempt: 1..MAX_RETRIES
    delay = (RETRY_BACKOFF_BASE_SECONDS ** attempt)
    time.sleep(delay)


def http_get_json(url: str, timeout: int = REQUEST_TIMEOUT_SECONDS) -> object:
    """
    GET robusto con retries ante timeouts o errores transitorios.
    """
    last_err: Optional[Exception] = None

    for attempt in range(1, MAX_RETRIES + 1):
        try:
            req = urllib.request.Request(
                url,
                headers={
                    "User-Agent": "FutureVestSeed/1.0 (+ITLA demo)",
                    "Accept": "application/json",
                },
            )
            with urllib.request.urlopen(req, timeout=timeout) as resp:
                raw = resp.read().decode("utf-8")
                return json.loads(raw)

        except (socket.timeout, TimeoutError) as e:
            last_err = e
            print(f"⏳ Timeout (attempt {attempt}/{MAX_RETRIES}) -> retrying...")
            _sleep_backoff(attempt)

        except urllib.error.HTTPError as e:
            # 429/5xx pueden ser temporales
            last_err = e
            code = getattr(e, "code", None)
            if code in (429, 500, 502, 503, 504):
                print(f"⚠️ HTTP {code} (attempt {attempt}/{MAX_RETRIES}) -> retrying...")
                _sleep_backoff(attempt)
                continue
            raise

        except Exception as e:
            last_err = e
            # error no esperado, probamos retry 1-2 veces igual
            print(f"⚠️ Error: {e} (attempt {attempt}/{MAX_RETRIES}) -> retrying...")
            _sleep_backoff(attempt)

    raise RuntimeError(f"Fallo GET tras {MAX_RETRIES} reintentos. Último error: {last_err}")


def fetch_worldbank_indicator(country_iso3: str, indicator_code: str) -> List[DataPoint]:
    """
    World Bank API retorna paginado:
    /country/{country}/indicator/{indicator}?format=json&per_page=200&page=1
    """
    points: List[DataPoint] = []
    per_page = 200
    page = 1
    total_pages = 1

    while page <= total_pages:
        endpoint = f"{WB_API_BASE}/country/{urllib.parse.quote(country_iso3)}/indicator/{urllib.parse.quote(indicator_code)}"
        url = f"{endpoint}?format=json&per_page={per_page}&page={page}"

        data = http_get_json(url)
        if not isinstance(data, list) or len(data) < 2:
            return points

        meta = data[0] if isinstance(data[0], dict) else {}
        total_pages = int(meta.get("pages", 1) or 1)

        rows = data[1]
        if not isinstance(rows, list):
            return points

        for r in rows:
            try:
                year = int(r.get("date"))
                val = r.get("value", None)
                if val is None:
                    points.append(DataPoint(year=year, value=None))
                else:
                    points.append(DataPoint(year=year, value=float(val)))
            except Exception:
                continue

        page += 1

    return points


def build_year_map(points: List[DataPoint]) -> Dict[int, Optional[float]]:
    """
    Devuelve mapa por año para 2020–2026.
    Si un año no está, queda None.
    """
    year_map: Dict[int, Optional[float]] = {y: None for y in range(START_YEAR, END_YEAR + 1)}
    for p in points:
        if START_YEAR <= p.year <= END_YEAR:
            year_map[p.year] = p.value
    return year_map


def fmt_value(v: Optional[float]) -> str:
    if v is None:
        return ""
    return f"{v:.6f}"


def main() -> int:
    OUTPUT_DIR.mkdir(parents=True, exist_ok=True)

    out_rows: List[List[str]] = []

    # CSV columns:
    # country_iso3,country_name,year,indicator_key,indicator_code,indicator_name,value,source,missing
    for iso3, country_name in COUNTRIES.items():
        for indicator_key, (code, indicator_name) in INDICATORS.items():
            print(f"📥 {iso3} - {indicator_key} ({code}) ...")
            try:
                points = fetch_worldbank_indicator(iso3, code)
                year_map = build_year_map(points)
            except Exception as e:
                # No tumbamos el proceso completo
                print(f"❌ Error obteniendo {iso3}/{code}: {e}")
                year_map = {y: None for y in range(START_YEAR, END_YEAR + 1)}

            for year in range(START_YEAR, END_YEAR + 1):
                val = year_map.get(year)
                missing = "1" if val is None else "0"
                out_rows.append([
                    iso3,
                    country_name,
                    str(year),
                    indicator_key,
                    code,
                    indicator_name,
                    fmt_value(val),
                    "WorldBank",
                    missing,
                ])

    with OUTPUT_CSV.open("w", newline="", encoding="utf-8") as f:
        w = csv.writer(f)
        w.writerow([
            "country_iso3",
            "country_name",
            "year",
            "indicator_key",
            "indicator_code",
            "indicator_name",
            "value",
            "source",
            "missing",
        ])
        w.writerows(out_rows)

    print(f"\n Seed generado: {OUTPUT_CSV}")
    print("Años sin data se exportan vacío y missing=1 (por ejemplo 2026).")
    return 0


if __name__ == "__main__":
    sys.exit(main())
