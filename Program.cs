
// Importa el paquete para trabajar con Entity Framework Core.
using Microsoft.EntityFrameworkCore;
// Importa el espacio de nombres donde se encuentra la clase ApplicationDbContext.
using Servicio_REST_CRUD_SQL.Data;

// Crea un objeto builder para configurar la aplicaci�n web ASP.NET Core.
var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.
// Agregar el servicio de ApplicationDbContext con la cadena de conexi�n
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    // Configura el servicio de base de datos usando el contexto de la aplicaci�n y la cadena de conexi�n definida.
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar los controladores para devolver respuestas en formato XML
builder.Services.AddControllers()
    .AddXmlSerializerFormatters(); // Agrega el formateador para que las respuestas de los controladores puedan ser en formato XML.
// End Cristhian

// Agrega el servicio de controladores para manejar las solicitudes HTTP.
builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Agrega servicios para la documentaci�n y exploraci�n de endpoints con Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Agrega el servicio para generar documentaci�n OpenAPI (Swagger).

var app = builder.Build(); // Construye la aplicaci�n web a partir de la configuraci�n definida en el builder.

// Configure the HTTP request pipeline.
// Configuraci�n del pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment()) // Verifica si la aplicaci�n est� en el entorno de desarrollo.
{
    app.UseSwagger(); // Habilita Swagger en el entorno de desarrollo.
    app.UseSwaggerUI(); // Habilita la interfaz de usuario de Swagger para explorar y probar los endpoints de la API.
}

app.UseHttpsRedirection(); // Habilita redirecci�n autom�tica de HTTP a HTTPS.

app.UseAuthorization(); // Habilita el middleware de autorizaci�n (no se especifica autenticaci�n aqu�, solo la verificaci�n de autorizaciones).

app.MapControllers(); // Mapea los controladores a los endpoints definidos en la API.

app.Run(); // Ejecuta la aplicaci�n.
