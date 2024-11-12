
// Importa el paquete para trabajar con Entity Framework Core.
using Microsoft.EntityFrameworkCore;
// Importa el espacio de nombres donde se encuentra la clase ApplicationDbContext.
using Servicio_REST_CRUD_SQL.Data;

// Crea un objeto builder para configurar la aplicación web ASP.NET Core.
var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.
// Agregar el servicio de ApplicationDbContext con la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    // Configura el servicio de base de datos usando el contexto de la aplicación y la cadena de conexión definida.
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar los controladores para devolver respuestas en formato XML
builder.Services.AddControllers()
    .AddXmlSerializerFormatters(); // Agrega el formateador para que las respuestas de los controladores puedan ser en formato XML.
// End Cristhian

// Agrega el servicio de controladores para manejar las solicitudes HTTP.
builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Agrega servicios para la documentación y exploración de endpoints con Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Agrega el servicio para generar documentación OpenAPI (Swagger).

var app = builder.Build(); // Construye la aplicación web a partir de la configuración definida en el builder.

// Configure the HTTP request pipeline.
// Configuración del pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment()) // Verifica si la aplicación está en el entorno de desarrollo.
{
    app.UseSwagger(); // Habilita Swagger en el entorno de desarrollo.
    app.UseSwaggerUI(); // Habilita la interfaz de usuario de Swagger para explorar y probar los endpoints de la API.
}

app.UseHttpsRedirection(); // Habilita redirección automática de HTTP a HTTPS.

app.UseAuthorization(); // Habilita el middleware de autorización (no se especifica autenticación aquí, solo la verificación de autorizaciones).

app.MapControllers(); // Mapea los controladores a los endpoints definidos en la API.

app.Run(); // Ejecuta la aplicación.
