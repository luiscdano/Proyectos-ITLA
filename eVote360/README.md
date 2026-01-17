# eVote360

Proyecto académico desarrollado en el marco de la carrera de **Desarrollo de Software**
en el **Instituto Tecnológico de las Américas (ITLA)**.

Este sistema web simula un entorno de **gestión electoral** donde se pueden administrar
**Partidos Políticos, Puestos Electivos, Elecciones, Candidaturas** y registrar **Votos**,
aplicando una arquitectura por capas con **ASP.NET Core MVC**, **Entity Framework Core**
y **ASP.NET Core Identity** para autenticación y control de acceso por roles.

---

## Objetivo del proyecto

Aplicar de forma práctica y estructurada los conocimientos adquiridos en:

- Programación Orientada a Objetos
- Arquitectura por capas (Domain / Application / Infrastructure / Web)
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core (Code First)
- ASP.NET Core Identity (Login, Registro, Roles)
- Validaciones con ViewModels
- Separación de responsabilidades (Controller ↔ Service ↔ Data)
- Seed de datos base (partidos, puestos, elección 2024 y candidaturas)

Todo esto con un enfoque **profesional y realista**, preparando el proyecto para
ser ampliado a un sistema electoral más completo.

---

## ¿De qué trata la aplicación?

eVote360 permite al usuario:

- Mantener un catálogo de **Partidos Políticos** (nombre, siglas, descripción, estado).
- Mantener un catálogo de **Puestos Electivos** (presidencia, vicepresidencia, etc.).
- Registrar **Elecciones** por año, tipo y fecha.
- Crear **Candidaturas** vinculadas a:
  - Elección
  - Puesto Electivo
  - Partido Político
- Registrar **Votos** asociados a:
  - Elección
  - Puesto Electivo
  - Candidatura
  - Fecha/Hora y token de voto opcional

Además, el sistema incorpora **validaciones** para evitar inconsistencias y
duplicidades en combinaciones clave.

---

## Funcionalidades principales

- **Autenticación y roles (Identity)**
  - Registro / Login
  - Control de acceso por rol (ej.: Admin para catálogos)

- **Partidos Políticos**
  - Crear, listar, editar, ver detalle y eliminar

- **Puestos Electivos**
  - Crear, listar, editar, ver detalle y eliminar

- **Elecciones**
  - Crear, listar, editar, ver detalle y eliminar

- **Candidaturas**
  - Crear, listar, editar, ver detalle y eliminar
  - Validación de duplicidad por combinación:
    **(Elección + Puesto + Partido)**

- **Votos**
  - Registro de votos (Elección + Puesto + Candidatura)
  - Listado de votos con fecha/hora y token

---

## Arquitectura del proyecto

El proyecto está organizado por capas, cumpliendo con el principio de separación de responsabilidades:

- **eVote360.Domain**
  - Entidades del dominio (Eleccion, PuestoElectivo, PartidoPolitico, Candidatura, Voto, AppUser)
  - Interfaces (repositorios)

- **eVote360.Application**
  - Contratos de servicios (por ejemplo `IGenericService<T>`)
  - Espacio para casos de uso / reglas de negocio (extensible)

- **eVote360.Infrastructure**
  - Persistencia con EF Core (`AppDbContext`)
  - Migraciones (Code First)
  - Implementación de servicios y repositorios (EF)

- **eVote360.Web**
  - ASP.NET Core MVC
  - Controladores
  - Vistas Razor
  - ViewModels y validaciones
  - UI con Bootstrap
  - Seed inicial de usuarios/roles (IdentitySeeder)

---

## Base de datos y Seed

- **Base de datos:** SQLite (entorno local)
- **Migraciones:** incluidas en el repositorio
- **Seed inicial realista:**
  - Partidos políticos base (PRM, PLD, FP, PRD)
  - Puestos electivos (Presidencia, Vicepresidencia, etc.)
  - Elección 2024 (Presidenciales y Congresuales)
  - Candidaturas base (Presidencia/Vicepresidencia por partido)

> Nota: la base de datos `.db` NO se sube al repositorio. Se genera localmente al ejecutar migraciones.

---

## Tecnologías y herramientas utilizadas

- **Lenguaje:** C#
- **Framework:** ASP.NET Core MVC (.NET 8)
- **ORM:** Entity Framework Core (Code First)
- **Autenticación:** ASP.NET Core Identity
- **Base de datos:** SQLite
- **Frontend:** Razor Views + Bootstrap
- **Arquitectura:** MVC + capas (Clean-ish layering)
- **Control de versiones:** Git & GitHub
- **Herramientas:**
  - Visual Studio Code
  - .NET CLI

---

## Acceso inicial (Administrador)

El sistema incluye un **seed automático de usuario administrador** al iniciar la aplicación por primera vez.

**Credenciales:**
- **Email:** luiscdano@gmail.com  
- **Rol:** Admin

> Por seguridad, la contraseña se define en el archivo de configuración o en el servicio de seed (`IdentitySeeder`).  
> Se recomienda cambiar la contraseña inmediatamente después del primer inicio de sesión.

Este usuario tiene acceso completo a:
- Gestión de Partidos Políticos
- Puestos Electivos
- Elecciones
- Candidaturas
- Registro y visualización de votos

---

## Aporte personal

Este proyecto me permitió reforzar:

- Diseño de un sistema completo con **entidades relacionadas**
- Integración real de **Identity + Roles** con acceso controlado
- Uso de **ViewModels** para formularios con combos y validación
- Construcción de un backend mantenible separando capas
- Seed de datos base para levantar el sistema rápidamente en cualquier entorno

---

## Posibles mejoras futuras

- Dashboard con resultados por partido/puesto (gráficas)
- Validación “1 voto por usuario por elección/puesto” (regla de negocio)
- Registro de votante y trazabilidad segura del voto
- Exportación de reportes (CSV / PDF)
- API REST para consumo externo
- Despliegue (Docker + Render/DO)

---

## Ejecución del proyecto

1. Clonar el repositorio o abrir la carpeta `eVote360` en Visual Studio Code.
2. Ejecutar comandos desde la carpeta `eVote360`:

```bash
dotnet restore
dotnet build
dotnet run --project eVote360.Web --launch-profile "https"