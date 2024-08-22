# Proyecto Microservicio FeedsService

Este proyecto tiene como objetivo crear una API para gestionar los usuarios y generar una autorizaci�n para otras API�s mediante JWT.

## Herramientas y Tecnolog�as

1. Microsoft Visual Studio 2022 o VS Code. : Necesario para poder abrir la soluci�n/proyecto.
2. Docker: Necesario para ejecutar el proyecto contenerizado
3. .NET 6 o superior: Necesario para ejecutar el proyecto .NET.
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
        (Debe de estar seleccionada la opci�n de IISExpress o Docker seg�n sea la nececidad de ejecuci�n)
     
### Docker

1. Construir y ejecutar el contenedor Docker para .NET

    ```
    docker build -t usuarioservice:latest .
    docker run -d -p 8080:8080 -p 8081:8081 --name usuarioservice_container usuarioservice:latest
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

- Escalabilidad: Se utiliza una arquitectura que favorece la escalabilidad y modularidad.
- Balanceo de Carga: Conciderar la implementaci�n de un API Gateway o alg�n mecanismo de balanceo de carga.

## Monitoreo

- Logs: Cnsiderar la implementaci�n de logs customizados en el proyecto.

## Estructura del Proyecto

- `Dockerfile`: Contiene la configuraci�n para construir la imagen Docker para la aplicaci�n .NET y Azure Functions.
- `/`: C�digo fuente de la aplicaci�n .NET.
- `Migrations/`: Scripts de migraci�n para la base de datos SQLite.
- `/Data`: Scripts para agregar datos iniciales a la base de datos.

## Funcionalidades

### Registro de Usuario

Los usuarios deben proporcionar los siguientes datos para el registro:
- Username: Nombre de usuario �nico.
- Password: Contrase�a segura.
- �ltima fecha de acceso: Fecha del �ltimo inicio de sesi�n.

### Acceso de Usuarios

Los usuarios pueden acceder a la plataforma con sus credenciales para gestionar sus feeds y acceder a la informaci�n disponible mediante un mecanismo de autenticaci�n basado en JWT.


## Desarrolado Por

- Victor Hugo Marmolejo B�ez

## Contribuyentes

## Licencia

Este proyecto est� licenciado bajo la Open Source.

## Contacto

Cualquier consulta oir favor escribe a mailto:vhmarbaez_one@hotmail.com.

