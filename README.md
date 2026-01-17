Repositorio central de **proyectos académicos y prácticos** desarrollados durante la carrera de **Desarrollo de Software** en el  
**Instituto Tecnológico de las Américas (ITLA)**.

Este repositorio funciona como un **monorepo académico**, donde cada carpeta representa un proyecto **independiente, ejecutable y mantenible de forma aislada**, siguiendo buenas prácticas de organización, claridad técnica y documentación.

El objetivo de este repositorio es **demostrar la evolución real de mis competencias**, combinando los requerimientos académicos del ITLA con criterios profesionales utilizados en entornos reales de desarrollo de software.

---

## Objetivos del Repositorio

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

# Arquitectura Global del Monorepo

## Visión general

Este repositorio sigue un enfoque de **monorepo académico-profesional**, donde cada proyecto es **independiente en ejecución**, pero todos comparten una **filosofía común de diseño, arquitectura y calidad técnica**.

El objetivo no es solo mostrar soluciones funcionales, sino demostrar:

- Criterio estructural  
- Separación de responsabilidades  
- Escalabilidad conceptual  
- Estilo de trabajo alineado con entornos reales de desarrollo de software  

Cada proyecto se comporta como si fuera un **repositorio productivo autónomo**, pero vive dentro de un ecosistema central que refleja evolución técnica progresiva.

---

## Principios de diseño aplicados

### 1. Separación por capas

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

### 2. Dominio como núcleo del sistema

El modelo de dominio representa el **lenguaje del problema**, no solo estructuras de base de datos.

Ejemplos reales del repositorio:
- `Candidatura`, `Eleccion`, `Voto` en **eVote360**  
- `Pais`, `MacroIndicador`, `Ranking` en **FutureVest**  

Estas entidades:
- Definen las reglas conceptuales del sistema  
- Son independientes del framework web o la base de datos  
- Pueden ser reutilizadas en APIs, servicios o microservicios futuros  

---

### 3. Dependencias dirigidas hacia adentro

Inspirado en principios de **Clean Architecture**:

```text
Web / UI
  ↓
Application / Business
  ↓
Domain

La infraestructura y la persistencia dependen del dominio, nunca al revés.

Esto permite:
	•	Escalar de SQLite a SQL Server, PostgreSQL o APIs externas
	•	Cambiar MVC por Web API o Frontend SPA
	•	Mantener el modelo central intacto

⸻

Estructura lógica común

Aunque cada proyecto tiene su propia organización, la mayoría sigue esta forma general:

ProyectoX/
│
├── ProyectoX.Domain
│   ├── Entities
│   ├── Interfaces
│   └── Reglas de negocio base
│
├── ProyectoX.Application / Business
│   ├── Servicios
│   ├── Casos de uso
│   └── Validaciones
│
├── ProyectoX.Infrastructure / Data
│   ├── DbContext
│   ├── Repositorios
│   ├── Migraciones
│   └── Seeders
│
├── ProyectoX.Web / UI
│   ├── Controllers
│   ├── ViewModels / DTOs
│   ├── Views / Frontend
│   └── Configuración de seguridad
│
└── README.md

Esto permite que cualquier proyecto nuevo que se agregue al monorepo herede una base estructural clara y coherente.

⸻

Patrón de servicios y repositorios

Se aplican patrones como:
	•	Repository Pattern
	•	Service Layer
	•	DTO / ViewModel
	•	Inyección de dependencias

Flujo conceptual:

Controller
   ↓
Servicio de aplicación
   ↓
Repositorio / EF Core
   ↓
Base de datos

Esto reduce:
	•	Acoplamiento
	•	Código duplicado
	•	Lógica en la capa de presentación

⸻

Seguridad y control de acceso

En proyectos que lo requieren (como eVote360):
	•	Autenticación con ASP.NET Identity
	•	Control por roles (ej. Admin)
	•	Protección de rutas críticas
	•	Separación entre usuarios públicos y administrativos

Esto refleja un enfoque realista de:
	•	Sistemas institucionales
	•	Plataformas gubernamentales
	•	Aplicaciones empresariales

⸻

Evolución técnica del repositorio

Este monorepo refleja un progreso real de complejidad y criterio técnico:

Fundamentos → Arquitectura → Patrones → Seguridad → Escalabilidad → Simulación → Dominio real

Ejemplo:
	•	Proyectos iniciales: validaciones, lógica estructurada, MVC
	•	Proyectos intermedios: separación por capas, modelos matemáticos, simulaciones
	•	Proyectos avanzados: identidad, roles, dominio institucional, diseño extensible

⸻

Proyectos incluidos (en crecimiento)
	•	ArtemisBanking – Plataforma bancaria digital (conceptual)
	•	RealEstateApp – Sistema de gestión inmobiliaria
	•	HorizonFutureVest – Análisis y ranking de inversión por países
	•	PredictorActivos – Predicción de tendencias financieras (acciones y criptomonedas)
	•	LinkUp – Red social académica
	•	eVote360 – Plataforma de votación electrónica

⸻

Proyectos incluidos

## Proyectos incluidos

## Proyectos incluidos

| Proyecto | Descripción | Tecnologías | Ejecutar |
|---------|-------------|-------------|----------|
| **[PredictorActivos](./PredictorActivos)** | Predicción de tendencia de activos (acciones/criptomonedas) usando SMA, Regresión Lineal y Momentum (ROC). | C#, ASP.NET Core MVC (.NET 8), Bootstrap | `dotnet run --project PredictorActivos.Web` |
| **[FutureVest](./FutureVest)** | Análisis, ranking y simulación de inversión por países basado en indicadores macroeconómicos y scoring ponderado. | C#, ASP.NET Core MVC (.NET 8), EF Core, Bootstrap | `dotnet run --project FutureVest.Web` |
| **[eVote360](./eVote360)** | Plataforma de votación electrónica con gestión de elecciones, partidos, candidaturas y registro de votos, incluyendo autenticación y control por roles (Admin). | C#, ASP.NET Core MVC (.NET 8), EF Core, Identity, SQLite, Bootstrap | `dotnet run --project eVote360.Web` |

⚠️ Algunos proyectos se encuentran en desarrollo o en evolución progresiva, lo cual refleja el proceso natural de aprendizaje y mejora continua.

⸻

Enfoque personal

Aunque me desenvuelvo con soltura en múltiples áreas del desarrollo de software, este repositorio no pretende mostrar perfección, sino criterio técnico, base sólida y crecimiento constante.

Cada proyecto fue desarrollado cumpliendo los requerimientos académicos, pero también cuestionando:
	•	¿Cómo se haría esto en un entorno profesional?
	•	¿Qué mejoraría la mantenibilidad?
	•	¿Qué aportaría valor real al usuario o al negocio?

Esa reflexión está documentada dentro de cada proyecto.

⸻

Tecnologías y herramientas frecuentes
	•	Visual Studio Code
	•	C#, .NET / ASP.NET Core
	•	Java / Java Swing
	•	Python
	•	SQL / SQLite / MySQL
	•	HTML, CSS, JavaScript
	•	Git & GitHub

(Las tecnologías específicas se detallan en el README de cada proyecto)

⸻

Nota final

Este repositorio representa el cierre de una etapa académica y el inicio de una transición clara hacia el entorno profesional.
Mi intención es que quien lo revise pueda entender cómo trabajo, cómo estructuro soluciones y cómo pienso como desarrollador.

⸻

Autor: Luis Emilio Cedano Martínez
Tecnólogo en Desarrollo de Software – ITLA

---

