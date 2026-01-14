# Predictor de Tendencia de Activos

Proyecto académico desarrollado en el marco de la carrera 

Esta aplicación web permite **analizar y predecir la tendencia de un activo financiero**
(acciones o criptomonedas) a partir de una serie de datos históricos, utilizando
diferentes **modelos matemáticos de predicción** implementados en una arquitectura
MVC moderna con ASP.NET Core.

---

## Objetivo del proyecto

Aplicar de forma práctica y estructurada los conocimientos adquiridos en:

- Programación Orientada a Objetos
- Arquitectura por capas
- ASP.NET Core MVC
- Separación de responsabilidades
- Validaciones con ViewModels
- Transferencia de datos mediante DTOs

Todo esto respetando los requerimientos funcionales definidos por el ITLA, pero con un enfoque **más cercano a un entorno profesional real**.

---

## ¿De qué trata la aplicación?

El sistema recibe **20 registros históricos exactos** (fecha y precio) de un activo
financiero y, según el **modo de predicción seleccionado**, calcula la posible
tendencia futura del activo, indicando si esta es **alcista o bajista**, junto con
los valores matemáticos que justifican el resultado.

La selección del modo se mantiene en memoria durante la ejecución del sistema,
permitiendo cambiar el comportamiento del cálculo sin modificar el formulario principal.

---

## Modos de predicción implementados

- **Media Móvil Simple (SMA Crossover)**  
  Compara una media móvil corta (5 periodos) contra una larga (20 periodos) para
  determinar la tendencia.

- **Regresión Lineal**  
  Ajusta una recta precio-tiempo y utiliza la pendiente para estimar el valor del
  día siguiente y su tendencia.

- **Momentum (ROC – Rate of Change)**  
  Calcula el cambio porcentual del precio usando un período fijo de 5 días (n = 5),
  mostrando además el **listado completo del ROC**, tal como indica el documento funcional.

---

## Arquitectura del proyecto

El proyecto está organizado en **dos capas principales**, cumpliendo con el principio
de separación de responsabilidades:

- **PredictorActivos.Web**  
  - ASP.NET Core MVC  
  - Controladores  
  - Vistas  
  - ViewModels con validaciones  
  - Interfaz de usuario (Bootstrap)

- **PredictorActivos.Business**  
  - Lógica de negocio  
  - Servicios de predicción  
  - DTOs  
  - Modelos  
  - Singleton para persistencia del modo de predicción

---

## Tecnologías y herramientas utilizadas

- **Lenguaje:** C#
- **Framework:** ASP.NET Core MVC (.NET 8)
- **Frontend:** Razor Views + Bootstrap
- **Arquitectura:** MVC + capas
- **Herramientas de desarrollo:**
  - Visual Studio Code
  - .NET CLI
  - Git & GitHub

---

## Aporte Personal

Este proyecto me permitió reforzar la importancia de:
	•	Diseñar primero la estructura antes de escribir código
	•	Separar correctamente la lógica de negocio de la presentación
	•	Traducir conceptos matemáticos a código entendible y reutilizable

Además, me ayudó a entender cómo una misma entrada de datos puede producir
resultados distintos dependiendo del modelo de análisis aplicado, algo clave
en sistemas financieros y de análisis de dato

## ¿Qué se puede obtener de este proyecto?

	•	Comprensión clara de cómo aplicar modelos matemáticos simples en software
	•	Uso correcto de arquitectura por capas
	•	Validaciones robustas de entrada de datos
	•	Separación limpia entre UI y lógica de negocio
	•	Código mantenible y escalable para futuras mejoras

## Posibles mejoras futuras

	•	Visualización gráfica de tendencias (charts)
	•	Persistencia de históricos en base de datos
	•	Exportación de resultados (CSV / PDF)
	•	Comparación simultánea entre distintos modos
	•	Integración con APIs de datos financieros en tiempo real

---

## Abrir en Navegar

	•	https://localhost:7062
	•	o http://localhost:5273

## Ejecución del proyecto

1. Clonar el repositorio o abrir la carpeta `PredictorActivos` en Visual Studio Code
2. Ejecutar el siguiente comando desde la raíz del proyecto:

```bash
dotnet run --project PredictorActivos.Web