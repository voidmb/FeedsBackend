# Proyecto Microservicio FeedsService

Este proyecto tiene como objetivo crear una API para gestionar feeds con m�ltiples topics, y proporcionar funcionalidades de paginaci�n y filtrado.

## Herramientas y Tecnolog�as

1. Microsoft Visual Studio 2022 o VS Code. : Necesario para poder abrir la soluci�n/proyecto.
2. Docker: Necesario para ejecutar el proyecto contenerizado
3. .NET 6 o superior: Necesario para ejecutar el proyecto .NET.
4. Node.js: Requerido para Azure Functions usando Node.js.
5. Azure Functions Core Tools: Para ejecutar funciones en el entorno local.
6. SQLite: necesario para pruebas locales.

## Configuraci�n del Entorno

### Obteneci�n del c�digo 

   - Clonar el repositorio: `git clone <url-del-repositorio>`

### .NET

#### Opci�n 1: Usando la L�nea de Comandos

1. Restaurar las dependencias

    ```bash
    dotnet restore
    ```

2. Ejecutar la aplicaci�n

    ```bash
    dotnet run
    ```

#### Opci�n 2: Usando Visual Studio 2022

1. Abrir el Proyecto en Visual Studio:
   - Abre Visual Studio y carga la soluci�n o el proyecto en el que deseas trabajar.

2. Restaurar Paquetes NuGet:
   - M�todo 1: Usar el Men� del Proyecto
     1. En el Explorador de Soluciones, haz clic derecho sobre el nombre del proyecto.
     2. Selecciona Restaurar paquetes del men� contextual.

3. Ejecutar la Aplicaci�n:
   - Seleccionar el Proyecto de Inicio
     1. En el Explorador de Soluciones, selecciona el proyecto que deseas ejecutar (deber�a estar marcado como Proyecto de Inicio o Startup Project).
   - Iniciar la Ejecuci�n     
     1. En la parte superior de Visual Studio, encuentra el bot�n verde de Iniciar (con un �cono de play) y haz clic en �l para compilar y ejecutar la aplicaci�n.
        (Debe de estar seleccionada la opci�n de IISExpress o Docker seg�n sea la nececidad de ejecuci�n).
     
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

## Autenticaci�m
     
Es necesario correr el API de Usuarios para generar un token de autenticaci�n para acceder a los endpoint.

## Pruebas

#### Opci�n 1: Usando la L�nea de Comandos

1. Navegar a la carpeta donde se encuentra el archivo .csproj y ejecutar:

    ```bash
    dotnet test
    ```

#### Opci�n 1: Usando Visual Studio 2022

1. Abrir el Explorador de Pruebas.
2. Ejecutar las pruebas o una por una con los botones adecuados. 

## Consideraciones para Alta Demanda y Disponibilidad

- Escalabilidad: Se utiliza Azure Functions para escalar autom�ticamente seg�n la demanda.
- Balanceo de Carga: Conciderar la implementaci�n de un API Gateway o alg�n mecanismo de balanceo de carga.
- Monitoreo: A trav�s de Azure Insights/Monitor para monitoreo del rendimiento, logs y an�lisis de m�tricas (Considerar el desarrollo de un log customizado).

## Monitoreo

- Azure Insights/Monitor: Configurar para el monitoreo en tiempo real y an�lisis de logs.
- Logs: Cnsiderar la implementaci�n de logs customizados en el proyecto.

## Estructura del Proyecto

- `Dockerfile`: Contiene la configuraci�n para construir la imagen Docker para la aplicaci�n .NET y Azure Functions.
- `/`: C�digo fuente de la aplicaci�n .NET.
- `functions/`: C�digo fuente de las funciones Azure.
- `Migrations/`: Scripts de migraci�n para la base de datos SQLite.
- `/Data`: Scripts para agregar datos iniciales a la base de datos.

## Funcionalidades

### Creaci�n de Feeds

Al crear un feed, se deben proporcionar los siguientes datos:
- Nombre de feed: Nombre del feed.
- Es P�blico: Indica si el feed es p�blico o privado.
- Topics: Lista de temas de interes relacionados con el feed.

Requisitos para los feeds:
- Los feeds privados solo son accesibles por el usuario creador.
- Un feed debe tener al menos un tema y un m�ximo de cinco.
- Los temas dentro de un mismo feed no deben repetirse.

### Listado de Feeds

El listado de feeds debe incluir:
- Nombre de feed
- Es P�blico
- Topics
- Fecha de creaci�n
- Fecha de actualizaci�n

Paginaci�n:
- Mostrar 10 registros por defecto.
- Ordenar por fecha de actualizaci�n/creaci�n.

### Listado de Feeds P�blicos

El listado de feeds p�blicos debe incluir:
- Nombre de feed
- Usuario creador
- P�blico
- Topics
- Fecha de creaci�n
- Fecha de actualizaci�n

Paginaci�n y Filtros:
- Mostrar 10 registros por defecto.
- Ordenar por fecha de actualizaci�n/creaci�n.
- Filtro por topic.

### Detalles de Feed

Los detalles de un feed deben mostrar:
- Nombre de feed
- P�blico
- Topics
- Fecha de creaci�n
- Fecha de actualizaci�n
- Recursos: Informaci�n sobre recursos asociados al feed.

Datos de Recursos:
- T�tulo
- Fecha
- Tipo
- Editorial
- Lenguajes

Paginaci�n de Recursos:
- Mostrar 20 registros m�s recientes.
- Ordenar por fecha.

### Actualizaci�n de Feeds

Los datos de un feed pueden ser actualizados, incluyendo:
- Nombre de feed
- Es P�blico
- Topics

### Eliminaci�n de Feeds y Topics

Los feeds y topics pueden ser eliminados siguiendo las pol�ticas establecidas para garantizar la integridad y consistencia del sistema.


## Desarrolado Por

- Victor Hugo Marmolejo B�ez

## Contribuyentes

## Licencia

Este proyecto est� licenciado bajo la Open Source.

## Contacto

Cualquier consulta oir favor escribe a mailto:vhmarbaez_one@hotmail.com.

