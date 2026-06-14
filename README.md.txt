# EstudioContableApp

Aplicación desarrollada en .NET MAUI siguiendo el patrón MVVM para la gestión de clientes de un estudio contable.

## Tecnologías utilizadas

* .NET MAUI
* CommunityToolkit.MVVM
* SQLite
* xUnit
* GitHub Actions

## Funcionalidades

* Alta de clientes
* Modificación de clientes
* Eliminación de clientes
* Visualización de detalle
* Persistencia local con SQLite
* Consumo de API REST
* Validaciones de datos

## Arquitectura

El proyecto utiliza:

* MVVM (Model View ViewModel)
* Repository Pattern
* Dependency Injection

## Testing

Se implementaron pruebas unitarias para:

* ClienteValidator
* ClienteRepository

## Integración continua

El proyecto cuenta con un workflow de GitHub Actions para compilación y ejecución automática de pruebas.
