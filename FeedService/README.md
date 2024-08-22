# Proyecto Microservicio FeedsService

Este proyecto tiene como objetivo crear una API para gestionar feeds con múltiples topics, y proporcionar funcionalidades de paginación y filtrado.

## Herramientas y Tecnologías

1. Microsoft Visual Studio 2022 o VS Code. : Necesario para poder abrir la solución/proyecto.
2. Docker: Necesario para ejecutar el proyecto contenerizado
3. .NET 6 o superior: Necesario para ejecutar el proyecto .NET.
4. Node.js: Requerido para Azure Functions usando Node.js.
5. Azure Functions Core Tools: Para ejecutar funciones en el entorno local.
6. SQLite: necesario para pruebas locales.

## Configuración del Entorno

### Obteneción del código 

   - Clonar el repositorio: `git clone <url-del-repositorio>`

### .NET

#### Opción 1: Usando la Línea de Comandos

1. Restaurar las dependencias

    ```bash
    dotnet restore
    ```

2. Ejecutar la aplicación

    ```bash
    dotnet run
    ```

#### Opción 2: Usando Visual Studio 2022

1. Abrir el Proyecto en Visual Studio:
   - Abre Visual Studio y carga la solución o el proyecto en el que deseas trabajar.

2. Restaurar Paquetes NuGet:
   - Método 1: Usar el Menú del Proyecto
     1. En el Explorador de Soluciones, haz clic derecho sobre el nombre del proyecto.
     2. Selecciona Restaurar paquetes del menú contextual.

3. Ejecutar la Aplicación:
   - Seleccionar el Proyecto de Inicio
     1. En el Explorador de Soluciones, selecciona el proyecto que deseas ejecutar (debería estar marcado como Proyecto de Inicio o Startup Project).
   - Iniciar la Ejecución     
     1. En la parte superior de Visual Studio, encuentra el botón verde de Iniciar (con un ícono de play) y haz clic en él para compilar y ejecutar la aplicación.
        (Debe de estar seleccionada la opción de IISExpress o Docker según sea la nececidad de ejecución).
     
### Docker

1. Construir y ejecutar el contenedor Docker para .NET

    ```
    docker build -t feedservice:latest .
    docker run -d -p 8080:8080 -p 8081:8081 --name feedervice_container feedservice:latest
    ```

2. Construir y ejecutar el contenedor Docker para Azure Functions

    ```
    docker build -t newspaper-functions-nodejs .
    docker run -p 7071:7071 newspaper-functions-nodejs
    ```


### Azure Functions

1. Instalar Azure Functions version 4

    ```
    npm install -g azure-functions-core-tools@4
    ```
 
2. Instalar Axios

    ```
    npm install axios
    ```
3. 2. Instalar Axios

    ```
    npm install jsonwebtoken
    ```
    
4. Restaurar las dependencias

    ```
    npm install
    ```

5. Ejecutar las funciones localmente

    ```
    func start
    ```

### SQLite

1. Migraciones y seeders

    Ejecutar los siguientes comandos para crear la base de datos y aplicar migraciones en caso de aplicar:

    ```
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

2. Datos iniciales

     La carga inicial de datos para este rpoyecto se encuentra en el archivo Data/SeedData.cs y se ejecuta al correr el proyecto

## Autenticacióm
     
Es necesario correr el API de Usuarios para generar un token de autenticación para acceder a los endpoint.

## Pruebas

#### Opción 1: Usando la Línea de Comandos

1. Navegar a la carpeta donde se encuentra el archivo .csproj y ejecutar:

    ```bash
    dotnet test
    ```

#### Opción 1: Usando Visual Studio 2022

1. Abrir el Explorador de Pruebas.
2. Ejecutar las pruebas o una por una con los botones adecuados. 

## Consideraciones para Alta Demanda y Disponibilidad

- Escalabilidad: Se utiliza Azure Functions para escalar automáticamente según la demanda.
- Balanceo de Carga: Conciderar la implementación de un API Gateway o algún mecanismo de balanceo de carga.
- Monitoreo: A través de Azure Insights/Monitor para monitoreo del rendimiento, logs y análisis de métricas (Considerar el desarrollo de un log customizado).

## Monitoreo

- Azure Insights/Monitor: Configurar para el monitoreo en tiempo real y análisis de logs.
- Logs: Cnsiderar la implementación de logs customizados en el proyecto.

## Estructura del Proyecto

- `Dockerfile`: Contiene la configuración para construir la imagen Docker para la aplicación .NET y Azure Functions.
- `/`: Código fuente de la aplicación .NET.
- `functions/`: Código fuente de las funciones Azure.
- `Migrations/`: Scripts de migración para la base de datos SQLite.
- `/Data`: Scripts para agregar datos iniciales a la base de datos.

## Funcionalidades

### Creación de Feeds

Al crear un feed, se deben proporcionar los siguientes datos:
- Nombre de feed: Nombre del feed.
- Es Público: Indica si el feed es público o privado.
- Topics: Lista de temas de interes relacionados con el feed.

Requisitos para los feeds:
- Los feeds privados solo son accesibles por el usuario creador.
- Un feed debe tener al menos un tema y un máximo de cinco.
- Los temas dentro de un mismo feed no deben repetirse.

### Listado de Feeds

El listado de feeds debe incluir:
- Nombre de feed
- Es Público
- Topics
- Fecha de creación
- Fecha de actualización

Paginación:
- Mostrar 10 registros por defecto.
- Ordenar por fecha de actualización/creación.

### Listado de Feeds Públicos

El listado de feeds públicos debe incluir:
- Nombre de feed
- Usuario creador
- Público
- Topics
- Fecha de creación
- Fecha de actualización

Paginación y Filtros:
- Mostrar 10 registros por defecto.
- Ordenar por fecha de actualización/creación.
- Filtro por topic.

### Detalles de Feed

Los detalles de un feed deben mostrar:
- Nombre de feed
- Público
- Topics
- Fecha de creación
- Fecha de actualización
- Recursos: Información sobre recursos asociados al feed.

Datos de Recursos:
- Título
- Fecha
- Tipo
- Editorial
- Lenguajes

Paginación de Recursos:
- Mostrar 20 registros más recientes.
- Ordenar por fecha.

### Actualización de Feeds

Los datos de un feed pueden ser actualizados, incluyendo:
- Nombre de feed
- Es Público
- Topics

### Eliminación de Feeds y Topics

Los feeds y topics pueden ser eliminados siguiendo las políticas establecidas para garantizar la integridad y consistencia del sistema.


## Desarrolado Por

- Victor Hugo Marmolejo Báez

## Contribuyentes

## Licencia

Este proyecto está licenciado bajo la Open Source.

## Contacto

Cualquier consulta oir favor escribe a mailto:vhmarbaez_one@hotmail.com.

