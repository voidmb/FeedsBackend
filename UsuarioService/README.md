# Proyecto Microservicio FeedsService

Este proyecto tiene como objetivo crear una API para gestionar los usuarios y generar una autorización para otras API´s mediante JWT.

## Herramientas y Tecnologías

1. Microsoft Visual Studio 2022 o VS Code. : Necesario para poder abrir la solución/proyecto.
2. Docker: Necesario para ejecutar el proyecto contenerizado
3. .NET 6 o superior: Necesario para ejecutar el proyecto .NET.
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
        (Debe de estar seleccionada la opción de IISExpress o Docker según sea la nececidad de ejecución)
     
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

#### Opción 1: Usando la Línea de Comandos

1. Navegar a la carpeta donde se encuentra el archivo .csproj y ejecutar:

    ```bash
    dotnet test
    ```

#### Opción 1: Usando Visual Studio 2022

1. Abrir el Explorador de Pruebas.
2. Ejecutar las pruebas o una por una con los botones adecuados. 

## Consideraciones para Alta Demanda y Disponibilidad

- Escalabilidad: Se utiliza una arquitectura que favorece la escalabilidad y modularidad.
- Balanceo de Carga: Conciderar la implementación de un API Gateway o algún mecanismo de balanceo de carga.

## Monitoreo

- Logs: Cnsiderar la implementación de logs customizados en el proyecto.

## Estructura del Proyecto

- `Dockerfile`: Contiene la configuración para construir la imagen Docker para la aplicación .NET y Azure Functions.
- `/`: Código fuente de la aplicación .NET.
- `Migrations/`: Scripts de migración para la base de datos SQLite.
- `/Data`: Scripts para agregar datos iniciales a la base de datos.

## Funcionalidades

### Registro de Usuario

Los usuarios deben proporcionar los siguientes datos para el registro:
- Username: Nombre de usuario único.
- Password: Contraseña segura.
- Última fecha de acceso: Fecha del último inicio de sesión.

### Acceso de Usuarios

Los usuarios pueden acceder a la plataforma con sus credenciales para gestionar sus feeds y acceder a la información disponible mediante un mecanismo de autenticación basado en JWT.


## Desarrolado Por

- Victor Hugo Marmolejo Báez

## Contribuyentes

## Licencia

Este proyecto está licenciado bajo la Open Source.

## Contacto

Cualquier consulta oir favor escribe a mailto:vhmarbaez_one@hotmail.com.

