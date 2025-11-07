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
2. Configurar la cadena de conexión en appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GestionPTDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
