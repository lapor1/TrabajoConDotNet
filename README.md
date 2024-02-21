# Nombre del Proyecto

Descripción breve del proyecto.

## Tecnologías Utilizadas

- ASP .NET Core
- C#
- Entity Framework Core
- PostgreSQL
- Visual Studio 2022

## Descripción

Este es un pequeño proyecto creado con el objetivo de aprender y practicar el desarrollo de API REST en C# utilizando ASP .NET Core. La persistencia de datos se gestiona mediante una base de datos PostgreSQL, y para interactuar con la base de datos se utiliza Entity Framework Core.

## Funcionalidades

### 1. Creación de Usuarios

Permite la creación de usuarios con la siguiente información:

- Nombre de usuario
- Ubicación en coordenadas en Latitud
- Ubicación en coordenadas en Longitud

### 2. Consulta del Día más Cálido

Ofrece la posibilidad de consultar, entre los próximos 14 días, cuál será el día más cálido para un usuario en particular.

## Instrucciones de Configuración

1. Clona el repositorio.
2. Abre el proyecto en Visual Studio 2022.
3. Configura la conexión a la base de datos PostgreSQL en el archivo de configuración correspondiente.
4. Ejecuta la aplicación.

## Uso

Proporciona ejemplos de cómo usar las API para crear usuarios y realizar consultas de días más cálidos.

```bash
# Ejemplo de creación de usuario
curl -X POST -H "Content-Type: application/json" -d '{"username": "ejemplo", "location": {"latitude": 123.45, "longitude": -67.89}}' http://localhost:5000/api/usuarios

# Ejemplo de consulta del día más cálido para un usuario específico
curl -X GET http://localhost:5000/api/consultas/dia-mas-calido?usuarioId=1
