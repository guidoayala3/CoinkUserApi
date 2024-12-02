# **User Registration API**

Esta es una API para registrar usuarios, que permite la creación y consulta de usuarios a través de endpoints REST. Utiliza PostgreSQL como base de datos y se genera documentación automáticamente con Swagger.
# **Endpoints**
## **1. POST /api/user**
Este endpoint se utiliza para crear un nuevo usuario.
### **Request Body (JSON):**
{
`  `"name": "Juan Pérez",
`  `"phone": "123456789",
`  `"address": "Calle Falsa 123",
`  `"country": "Colombia",
`  `"department": "Antioquia",
`  `"municipality": "Medellín"
}
### **Response:**
Status Code: `201 Created`
Body: El ID del usuario creado.
## **2. GET /api/users**
Este endpoint se utiliza para obtener todos los usuarios registrados.
### **Response:**
Status Code: `200 OK`
Body (JSON):

[
`  `{
`    `"id": 1,
`    `"name": "Juan Pérez",
`    `"phone": "123456789",
`    `"address": "Calle Falsa 123",
`    `"country": "Colombia",
`    `"department": "Antioquia",
`    `"municipality": "Medellín"
`  `}
]
# **Esquemas**
## **UserDto**
{
`  `"id": "int",
`  `"name": "string",
`  `"phone": "string",
`  `"address": "string",
`  `"country": "string",
`  `"department": "string",
`  `"municipality": "string"
}
# **Instalación y ejecución local**
## **Clonar el repositorio**
1\. Clona el repositorio desde GitHub:
`git clone https://github.com/guidoayala3/CoinkUserApi`
2\. Cambia al branch `develop`:
`git checkout develop`
## **Configuración del entorno**
1\. Crea un archivo `.env` en la raíz del proyecto con las siguientes variables de entorno:
\```ini
DB\_HOST=localhost
DB\_NAME=nombre\_de\_base\_de\_datos
DB\_USER=usuario
DB\_PASSWORD=contraseña
\```
2\. Asegúrate de tener PostgreSQL en ejecución y con la base de datos configurada según las variables anteriores.
## **Ejecutar la aplicación**
1\. Restaura las dependencias:
`dotnet restore`
2\. Compila el proyecto:
`dotnet build`
3\. Ejecuta la aplicación:
`dotnet run`
La API estará disponible en `http://localhost:5227` (por defecto).
## **Acceder a la documentación de Swagger**
Si estás ejecutando la aplicación en un entorno de desarrollo, puedes acceder a la documentación de Swagger en:
`http://localhost:5227/swagger`

# **Tecnologías utilizadas**
\- \*\*.NET 8.0\*\*: Framework para el desarrollo de la API.
\- \*\*PostgreSQL\*\*: Base de datos relacional utilizada para almacenar la información de los usuarios.
\- \*\*Swagger\*\*: Generación automática de documentación para la API.
\- \*\*FluentValidation\*\*: Validación de los modelos de entrada.
\- \*\*AutoMapper\*\*: Para mapeo entre modelos si es necesario.
# **Licencia**
Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.
