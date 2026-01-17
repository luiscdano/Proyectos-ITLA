# Proyectos-ITLA

Repositorio central de **proyectos académicos y prácticos** desarrollados durante la carrera de **Desarrollo de Software** en el  
**Instituto Tecnológico de las Américas (ITLA)**.

Este repositorio funciona como un **monorepo académico**, donde cada carpeta representa un proyecto **independiente, ejecutable y mantenible de forma aislada**, siguiendo buenas prácticas de organización, claridad técnica y documentación.

El objetivo de este repositorio es **demostrar la evolución real de mis competencias**, combinando los requerimientos académicos del ITLA con criterios profesionales utilizados en entornos reales de desarrollo de software.

---

## Objetivos del repositorio

- Centralizar los proyectos asignados en **distintas materias** del ITLA.
- Aplicar de forma práctica conceptos de:
  - Programación orientada a objetos
  - Arquitecturas por capas
  - MVC / separación de responsabilidades
  - Buenas prácticas de código y documentación
- Mostrar **criterio técnico**, capacidad de análisis y pensamiento estructurado.
- Reflejar no solo lo aprendido, sino **cómo pienso como desarrollador** y cómo mejoraría cada solución.

---

## Estructura general

Cada proyecto vive en su **propia carpeta**, y puede abrirse y ejecutarse **de manera independiente** desde Visual Studio Code.

Estructura esperada por proyecto:

- `README.md` propio  
  - Descripción del proyecto  
  - Objetivo académico  
  - Demo o capturas (cuando aplica)  
  - Setup / requisitos  
  - Arquitectura y decisiones técnicas  

- `src/` o estructura MVC estándar  
  - Código fuente organizado por capas o responsabilidades  

- `tests/` (si aplica)  
  - Pruebas unitarias o de validación  

- Instrucciones claras **1-2-3** para ejecutar el proyecto en VS Code  

- **Aporte personal**  
  - Qué aprendí desarrollando el proyecto  
  - Qué decisión técnica fue clave  
  - Qué funcionalidad innovadora o creativa hubiese agregado para sobresalir aún más  

---

# Arquitectura global del monorepo

## Visión general

Este repositorio sigue un enfoque de **monorepo académico-profesional**, donde cada proyecto es **independiente en ejecución**, pero todos comparten una **filosofía común de diseño, arquitectura y calidad técnica**.

El objetivo no es solo mostrar soluciones funcionales, sino demostrar:

- Criterio estructural  
- Separación de responsabilidades  
- Escalabilidad conceptual  
- Estilo de trabajo alineado con entornos reales de desarrollo de software  

---

## Principios de diseño aplicados

### 1) Separación por capas

Los sistemas se organizan evitando la dependencia directa entre:

- **Presentación (UI / Web / Consola)**  
- **Lógica de negocio / Application**  
- **Acceso a datos / Infrastructure**  
- **Dominio del problema / Domain**  

Esto permite:
- Cambiar la interfaz sin afectar la lógica  
- Sustituir la base de datos sin reescribir reglas de negocio  
- Testear componentes de forma aislada  

---

### 2) Dominio como núcleo del sistema

El modelo de dominio representa el **lenguaje del problema**, no solo estructuras de base de datos.

Ejemplos reales del repositorio:
- `Candidatura`, `Eleccion`, `Voto` en **eVote360**  
- `Pais`, `MacroIndicador`, `Ranking` en **FutureVest**  

---

## Proyectos incluidos (en crecimiento)

- **ArtemisBanking** – Plataforma bancaria digital (conceptual)
- **RealEstateApp** – Sistema de gestión inmobiliaria
- **HorizonFutureVest** – Análisis y ranking de inversión por países
- **PredictorActivos** – Predicción de tendencias financieras (acciones y criptomonedas)
- **LinkUp** – Red social académica
- **eVote360** – Plataforma de votación electrónica

---

## Proyectos incluidos

| Proyecto | Descripción | Tecnologías | Ejecutar |
|----------|-------------|-------------|----------|
| PredictorActivos | Predicción de tendencia de activos (acciones/criptomonedas) usando SMA, Regresión Lineal y Momentum (ROC). | C#, ASP.NET Core MVC (.NET 8), Bootstrap | `dotnet run --project PredictorActivos.Web` |
| FutureVest | Análisis, ranking y simulación de inversión por países basado en indicadores macroeconómicos y scoring ponderado. | C#, ASP.NET Core MVC (.NET 8), EF Core, Bootstrap | `dotnet run --project FutureVest.Web` |
| eVote360 | Plataforma de votación electrónica con gestión de elecciones, partidos, candidaturas y registro de votos, con autenticación y control por roles (Admin). | C#, ASP.NET Core MVC (.NET 8), EF Core, Identity, SQLite, Bootstrap | `dotnet run --project eVote360.Web` |

### Abrir proyectos

- **[PredictorActivos](./PredictorActivos)**
- **[FutureVest](./FutureVest)**
- **[eVote360](./eVote360)**

> ⚠️ Algunos proyectos se encuentran en desarrollo o en evolución progresiva, lo cual refleja el proceso natural de aprendizaje y mejora continua.

---

## Enfoque personal

Aunque me desenvuelvo con soltura en múltiples áreas del desarrollo de software, este repositorio **no pretende mostrar perfección**, sino **criterio técnico, base sólida y crecimiento constante**.

---

## Tecnologías y herramientas frecuentes

- Visual Studio Code  
- C#, .NET / ASP.NET Core  
- Java / Java Swing  
- Python  
- SQL / SQLite / MySQL  
- HTML, CSS, JavaScript  
- Git & GitHub  

---

## Nota final

Este repositorio representa el cierre de una etapa académica y el inicio de una transición clara hacia el **entorno profesional**.  
Mi intención es que quien lo revise pueda entender **cómo trabajo, cómo estructuro soluciones y cómo pienso como desarrollador**.

---

**Autor:** Luis Emilio Cedano Martínez  
Tecnólogo en Desarrollo de Software – ITLA  