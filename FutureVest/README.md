# FutureVest

Proyecto académico desarrollado en el marco de la carrera de **Desarrollo de Software**
en el **Instituto Tecnológico de las Américas (ITLA)**.

Esta aplicación web permite **analizar la factibilidad de inversión en distintos países**
a partir de indicadores macroeconómicos, utilizando un **modelo matemático de scoring
ponderado**, normalización de datos y simulaciones dinámicas, implementado bajo una
arquitectura MVC moderna con ASP.NET Core.

---

## Objetivo del proyecto

Aplicar de forma práctica y estructurada los conocimientos adquiridos en:

- Programación Orientada a Objetos
- Arquitectura por capas
- ASP.NET Core MVC
- Entity Framework Core (Code First)
- Separación de responsabilidades
- Validaciones mediante ViewModels
- Transferencia de datos con DTOs
- Modelos matemáticos aplicados al análisis financiero

Todo esto respetando los **requerimientos funcionales definidos por el ITLA**, pero con
un enfoque **más cercano a un entorno profesional real**, orientado al análisis de datos
y toma de decisiones.

---

## ¿De qué trata la aplicación?

FutureVest es un sistema de análisis que permite al usuario:

- Registrar **países** y sus **indicadores macroeconómicos** por año.
- Configurar **pesos ponderados** para cada macroindicador.
- Calcular un **ranking de países** basado en un modelo de scoring.
- Estimar una **tasa de retorno** asociada a cada país.
- Ejecutar **simulaciones de ranking**, modificando los pesos sin alterar los valores
  originales del sistema.

La aplicación no solo muestra resultados, sino que valida y explica **por qué**
un país es o no elegible para el ranking, garantizando transparencia en el cálculo.

---

## Funcionalidades principales

- **Home / Ranking**
  - Selección dinámica del año más reciente con datos disponibles.
  - Validación de que la suma de los pesos de los macroindicadores sea igual a 1.
  - Validación de países elegibles según indicadores completos.
  - Visualización del ranking ordenado por scoring descendente.
  - Cálculo automático de la tasa estimada de retorno.

- **Mantenimiento de Países**
  - Crear, listar, editar y eliminar países.
  - Validaciones de campos obligatorios (nombre y código ISO).

- **Mantenimiento de Macroindicadores**
  - Crear, editar y eliminar macroindicadores.
  - Configuración de peso y criterio “más alto es mejor”.
  - Validación estricta para que la suma total de pesos no supere 1.

- **Indicadores por País**
  - Registro de valores macroeconómicos por país y año.
  - Validación para evitar duplicidad de indicadores en un mismo período.
  - Filtros por país y año.
  - Edición limitada únicamente al valor del indicador.

- **Configuración de Tasa de Retorno**
  - Definición de tasa mínima y máxima estimada.
  - Validación de rangos correctos.
  - Valores por defecto aplicados cuando no hay configuración explícita.

- **Simulador de Ranking**
  - Creación de un conjunto alternativo de pesos para simulación.
  - No modifica los macroindicadores originales.
  - Permite probar distintos escenarios de inversión.
  - Ranking independiente del ranking principal.

---

## Arquitectura del proyecto

El proyecto está organizado en **tres capas principales**, cumpliendo con el principio
de separación de responsabilidades:

- **FutureVest.Web**
  - ASP.NET Core MVC
  - Controladores
  - Vistas Razor
  - ViewModels con validaciones
  - Interfaz de usuario con Bootstrap
  - Layout común para toda la aplicación

- **FutureVest.Business**
  - Lógica de negocio
  - Servicios de cálculo
  - DTOs para transferencia de datos
  - Reglas de validación y modelos matemáticos

- **FutureVest.Data**
  - Entity Framework Core (Code First)
  - Entidades
  - DbContext
  - Migraciones
  - Seeder de datos

---

## Modelo matemático utilizado

- **Normalización Min-Max**
  - Diferenciando indicadores donde “más alto es mejor” y “más bajo es mejor”.
  - Uso del valor 0.5 cuando el mínimo y máximo coinciden.

- **Cálculo de sub-puntajes**
  - Valor normalizado × peso del macroindicador.

- **Scoring total**
  - Sumatoria de los sub-puntajes.
  - Rango válido entre 0 y 1.

- **Tasa estimada de retorno**
  - Función lineal basada en el scoring.
  - Parámetros configurables por el usuario.

---

## Tecnologías y herramientas utilizadas

- **Lenguaje:** C#
- **Framework:** ASP.NET Core MVC (.NET 8)
- **ORM:** Entity Framework Core (Code First)
- **Base de datos:** SQLite (entorno local)
- **Frontend:** Razor Views + Bootstrap
- **Arquitectura:** MVC + capas
- **Control de versiones:** Git & GitHub
- **Herramientas de desarrollo:**
  - Visual Studio Code
  - .NET CLI

---

## Aporte personal

Este proyecto permitió profundizar en:

- La traducción de **modelos matemáticos teóricos** a código funcional.
- La importancia de **validaciones de negocio claras** antes de generar resultados.
- El diseño de sistemas **extensibles y mantenibles**, más allá de cumplir una tarea.
- La separación correcta entre datos, lógica y presentación.

Además, se agregó un **módulo externo de generación de datos (seed)**, pensado para
facilitar pruebas y simulaciones con información macroeconómica realista.

---

## ¿Qué se puede obtener de este proyecto?

- Comprensión clara del uso de **modelos de scoring ponderado**.
- Aplicación práctica de **normalización de datos**.
- Uso correcto de **arquitectura por capas**.
- Validaciones robustas orientadas al negocio.
- Base sólida para proyectos de análisis financiero o económico.

---

## Posibles mejoras futuras

- Consumo de APIs reales (Banco Mundial, FMI)
- Visualización gráfica de rankings y comparaciones.
- Exportación de resultados (CSV / PDF).
- Autenticación de usuarios y perfiles.
- Persistencia en bases de datos más robustas.
- Dashboard analítico avanzado.

---

## Ejecución del proyecto

1. Clonar el repositorio o abrir la carpeta `FutureVest` en Visual Studio Code.
2. Ejecutar los siguientes comandos desde la carpeta `FutureVest/src`:

```bash
dotnet restore
dotnet build
dotnet run --project FutureVest.Web
