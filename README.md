# 🐾 Veterinary API - Sistema de Gestión de Inventario Veterinario

Una API REST desarrollada en ASP.NET Core 8.0 para la gestión integral de inventario de medicamentos veterinarios, incluyendo control de stock, alertas automáticas, y seguimiento de movimientos.

## 📋 Características Principales

- **Gestión de Medicamentos**: CRUD completo con información detallada
- **Control de Proveedores**: Administración de información de proveedores
- **Movimientos de Inventario**: Registro de entradas y salidas con trazabilidad
- **Sistema de Alertas**: Notificaciones automáticas para vencimientos y stock bajo
- **Reportes**: Análisis de consumo y medicamentos más utilizados
- **Autenticación**: Sistema de usuarios con roles (Administrador/Empleado)
- **Exportación**: Generación de reportes en formato CSV

## 🛠️ Tecnologías Utilizadas

- **Framework**: ASP.NET Core 8.0
- **Base de Datos**: MySQL con Entity Framework Core
- **ORM**: Entity Framework Core 8.0
- **Documentación**: Swagger/OpenAPI
- **Contenedor**: Docker (puerto 10000)

## 📁 Estructura del Proyecto

```
VeterinaryAPI/
├── Controllers/           # Controladores de la API
│   ├── AlertasController.cs
│   ├── MedicamentosController.cs
│   ├── MovimientosInventarioController.cs
│   ├── ProveedoresController.cs
│   ├── ReportesController.cs
│   └── UsuariosController.cs
├── Models/               # Modelos de datos
│   ├── Medicamento.cs
│   ├── Proveedor.cs
│   ├── MovimientoInventario.cs
│   ├── Usuario.cs
│   └── Alerta.cs
├── DTO/                  # Data Transfer Objects
├── Data/                 # Contexto de base de datos
├── Migrations/           # Migraciones de EF Core
└── Dockerfile           # Configuración Docker
```

## 🚀 Instalación y Configuración

### Prerrequisitos

- .NET 8.0 SDK
- MySQL Server
- Docker (opcional)

### Configuración Local

1. **Clonar el repositorio**
   ```bash
   git clone <repository-url>
   cd VeterinaryAPI
   ```

2. **Configurar la cadena de conexión**
   
   Editar `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;database=veterinary_db;user=root;password=your_password"
     }
   }
   ```

3. **Restaurar paquetes**
   ```bash
   dotnet restore
   ```

4. **Ejecutar migraciones**
   ```bash
   dotnet ef database update
   ```

5. **Ejecutar la aplicación**
   ```bash
   dotnet run
   ```

### Configuración con Docker

```bash
docker build -t veterinary-api .
docker run -p 10000:10000 veterinary-api
```

## 📚 Endpoints de la API

### 🏥 Medicamentos
- `GET /api/medicamentos` - Listar todos los medicamentos
- `GET /api/medicamentos/{id}` - Obtener medicamento por ID
- `POST /api/medicamentos` - Crear nuevo medicamento
- `PUT /api/medicamentos/{id}` - Actualizar medicamento
- `DELETE /api/medicamentos/{id}` - Eliminar medicamento

### 🏢 Proveedores
- `GET /api/proveedores` - Listar proveedores
- `GET /api/proveedores/{id}` - Obtener proveedor por ID
- `POST /api/proveedores` - Crear proveedor
- `PUT /api/proveedores/{id}` - Actualizar proveedor
- `DELETE /api/proveedores/{id}` - Eliminar proveedor

### 📦 Movimientos de Inventario
- `GET /api/movimientosinventario` - Historial de movimientos
- `POST /api/movimientosinventario` - Registrar movimiento (entrada/salida)

### 🚨 Alertas
- `GET /api/alertas/vencimientos-proximos` - Medicamentos próximos a vencer (15 días)
- `GET /api/alertas/vencidos` - Medicamentos vencidos
- `GET /api/alertas/stock-minimo` - Medicamentos con stock bajo

### 📊 Reportes
- `GET /api/reportes/consumo-mensual?año={año}&mes={mes}` - Consumo mensual
- `GET /api/reportes/mas-utilizados` - Top 10 medicamentos más utilizados
- `GET /api/reportes/exportar-excel` - Exportar datos en CSV

### 👥 Usuarios
- `POST /api/usuarios/registrar` - Registrar nuevo usuario
- `POST /api/usuarios/login` - Iniciar sesión
- `POST /api/usuarios/logout` - Cerrar sesión
- `GET /api/usuarios/ping` - Verificar estado de la API

## 🗄️ Modelo de Datos

### Medicamento
```json
{
  "id": 1,
  "codigo": "MED001",
  "nombre": "Antibiótico Canino",
  "cantidad": 50,
  "stockMinimo": 10,
  "dosis": "5mg/kg",
  "tipoAnimal": "Canino",
  "fechaVencimiento": "2024-12-31",
  "proveedorId": 1,
  "proveedorNombre": "Farmacia Veterinaria XYZ"
}
```

### Proveedor
```json
{
  "id": 1,
  "nombre": "Farmacia Veterinaria XYZ",
  "telefono": "555-0123",
  "email": "contacto@farmvet.com",
  "direccion": "Calle Principal 123"
}
```

### Movimiento de Inventario
```json
{
  "id": 1,
  "medicamentoId": 1,
  "medicamentoNombre": "Antibiótico Canino",
  "fecha": "2024-06-20T10:30:00Z",
  "cantidad": 10,
  "tipoMovimiento": "Salida",
  "observaciones": "Dispensado para tratamiento"
}
```

## 🔐 Autenticación

El sistema utiliza un mecanismo básico de autenticación con hash SHA256 para las contraseñas. Los usuarios pueden tener dos roles:

- **Administrador**: Acceso completo al sistema
- **Empleado**: Acceso limitado (por defecto)

### Ejemplo de registro:
```json
{
  "username": "admin",
  "password": "password123",
  "role": "Administrador"
}
```

## 🚨 Sistema de Alertas

El sistema genera alertas automáticas para:

1. **Vencimientos Próximos**: Medicamentos que vencen en los próximos 15 días
2. **Medicamentos Vencidos**: Productos que ya han expirado
3. **Stock Mínimo**: Medicamentos que han alcanzado el nivel mínimo de inventario

## 📈 Funcionalidades de Reportes

- **Consumo Mensual**: Análisis de medicamentos utilizados por mes
- **Medicamentos Más Utilizados**: Top 10 de productos con mayor rotación
- **Exportación CSV**: Descarga de datos completos del inventario

## 🔧 Configuración CORS

La API está configurada con políticas CORS permisivas para desarrollo:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
```

## 🐳 Docker

La aplicación está containerizada y se ejecuta en el puerto 10000:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://*:10000
```

## 📝 Notas de Desarrollo

- La aplicación utiliza MySQL como base de datos principal
- Las migraciones de Entity Framework están incluidas
- Swagger UI disponible en `/swagger` para documentación interactiva
- Logs configurados para ambiente de desarrollo

## 🤝 Contribución

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 📞 Soporte

Para soporte técnico o consultas, contactar al equipo de desarrollo.

---

**Desarrollado con ❤️ para la gestión eficiente de inventarios veterinarios**
