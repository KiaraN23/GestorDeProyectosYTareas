# GestorDeProyectosYTareas
API REST que gestiona Proyectos y Tareas con persistencia en BD, validaciones, paginación y pruebas.

## Requisitos previos

Antes de ejecutar el proyecto localmente, asegúrate de tener instalado:

- [.NET SDK 8.0+]
- [SQL Server / SQL Server Express]
- [Visual Studio 2022] o [VS Code]

## Configuración y ejecución local

1. **Clonar el repositorio:**

   ```bash
   git clone https://github.com/KiaraN23/GestorDeProyectosYTareas.git
   cd GestionProyectosYTareas
2. **Configurar la cadena de conexión en appsettings.json:**

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GestProyectoTarea;Trusted_Connection=True;TrustServerCertificate=True;"
}

3. **Aplicar las migraciones y crear la base de datos:**

cd GestionProyectosYTareas.API
dotnet ef database update

4. **Ejecutar la API:**

dotnet run

La API se iniciará en:

http://localhost:5000

https://localhost:7000

5. **Probar en Swagger:**
Una vez ejecutada, abre en tu navegador:

https://localhost:7000/swagger


## Estructura del proyecto

GestionProyectosYTareas/
│
├── GestionProyectosYTareas.API/            
│   ├── Controllers/
│   ├── DTOs/
│   ├── Program.cs
│   └── appsettings.json
│
├── GestionProyectosYTareas.Application/     
│   ├── Interfaces/
│   └── Services/
│
├── GestionProyectosYTareas.Domain/          
│   ├── Entities/
│   └── Enums/
│
├── GestionProyectosYTareas.Infrastructure/  
│   ├── AppDbContext/
│   ├── Interfaces/
│   └── Repositories/
│
├── GestionProyectosYTareas.Tests/           
│   ├── Services/
│   └── Integration/
│
└── README.md


## Decisiones técnicas y trade-offs

**Arquitectura**

Se utilizó una arquitectura en capas:
API → Application → Infrastructure → Domain

Esto favorece la separación de responsabilidades y testabilidad.

ORM y Base de datos

Se implementó Entity Framework Core con SQL Server.

Se incluye un DbInitializer para sembrar datos de ejemplo (2 proyectos y 10 tareas).

**Validaciones**

Uso de DataAnnotations en DTOs (se podría sustituir por FluentValidation en versiones futuras).

Verificación de campos obligatorios y rangos (por ejemplo, Title con mínimo 3 caracteres).

**Concurrencia**

Control mediante la propiedad RowVersion en TaskItem para actualizaciones seguras (optimistic concurrency).

**Paginación y filtrado**

Los endpoints de Projects y Tasks soportan:

page, pageSize, search, sort

Filtros por status, projectId, q

Respuestas incluyen metadatos: totalItems, totalPages, page, pageSize.

**Logging**

Logging básico implementado con ILogger y correlación de RequestId.

**Trade-offs**

No se utilizó AutoMapper para mantener control manual sobre los DTOs y simplificar dependencias.

No se agregó autenticación JWT para mantener el enfoque en la funcionalidad CRUD y la calidad técnica.

Se usaron Enum.TryParse en lugar de Enum.Parse para mayor tolerancia de errores en inputs.


## Endpoints principales

**Proyectos**
Método		
GET	/api/projects?page=1&pageSize=10&search=...	
GET	/api/projects/{id}	
POST	/api/projects	
PUT	/api/projects/{id}	
DELETE	/api/projects/{id}	

**Tareas**
Método		
GET	/api/tasks?projectId=...&status=...	
GET	/api/tasks/{id}	
POST	/api/tasks	
PUT	/api/tasks/{id}	
DELETE	/api/tasks/{id}


## Pruebas

El proyecto incluye pruebas unitarias y de integración usando:

xUnit

Moq

FluentAssertions

WebApplicationFactory para tests end-to-end

**Ejecutar pruebas:**

dotnet test


## Cobertura estimada:

Servicios y validaciones: ✅

Integración básica (CRUD completo): ✅

Cobertura mínima orientativa: ~60%


## Migraciones EF Core

**Para crear nuevas migraciones:**

dotnet ef migrations add NombreMigracion -p GestionProyectosYTareas.Infrastructure -s GestionProyectosYTareas.API
dotnet ef database update -p GestionProyectosYTareas.Infrastructure -s GestionProyectosYTareas.API


**Limpiar y recrear la base de datos:**

dotnet ef database drop -p GestionProyectosYTareas.Infrastructure -s GestionProyectosYTareas.API
dotnet ef database update -p GestionProyectosYTareas.Infrastructure -s GestionProyectosYTareas.API
