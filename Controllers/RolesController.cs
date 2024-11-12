// Importa el espacio de nombres que contiene los componentes necesarios
// para trabajar con controladores en ASP.NET Core.
using Microsoft.AspNetCore.Mvc;
// Importa el espacio de nombres de Entity Framework Core,
// que proporciona funcionalidad para interactuar con bases de datos.
using Microsoft.EntityFrameworkCore;
using Servicio_REST_CRUD_SQL.Data;
using Servicio_REST_CRUD_SQL.Models;

// Importa los espacios de nombres necesarios para trabajar con colecciones genéricas y operaciones asincrónicas.
/*using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/

namespace Servicio_REST_CRUD_SQL.Controllers
{
    // Definimos la ruta base para este controlador API. La ruta será 'api/Roles'.
    [Route("api/[controller]")]
    // Indicamos que esta clase es un controlador API, y que el modelo de datos se basa en las rutas de la API.
    [ApiController]
    // Definimos que este controlador produce respuestas en formato XML.
    [Produces("application/xml")]
    public class RolesController : ControllerBase
    {
        // Declaramos el contexto de la base de datos para interactuar con la tabla de roles.
        private readonly ApplicationDbContext _context;

        // Constructor del controlador que recibe el contexto de la base de datos y lo asigna a la variable _context.
        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para obtener todos los roles (GET: api/Roles)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class_Role>>> GetRoles()
        {
            // Recuperamos todos los roles de la base de datos de manera asincrónica y los devolvemos como resultado.
            return await _context.tb_Roles.ToListAsync();
        }

        // Acción para obtener un rol específico por ID (GET: api/Roles/5)
        [HttpGet("{id}")]
        public async Task<ActionResult<Class_Role>> GetRole(int id)
        {
            // Buscamos el rol en la base de datos utilizando el ID proporcionado.
            var role = await _context.tb_Roles.FindAsync(id);

            // Si no se encuentra el rol, devolvemos un resultado "NotFound".
            if (role == null)
            {
                return NotFound();
            }

            // Si el rol se encuentra, lo devolvemos como resultado.
            return role;
        }
        // Acción para crear un nuevo rol (POST: api/Roles)
        [HttpPost]
        public async Task<ActionResult<Class_Role>> PostRole(Class_Role role)
        {
            // Agregamos el nuevo rol a la tabla de Roles en la base de datos.
            _context.tb_Roles.Add(role);

            // Guardamos los cambios en la base de datos de manera asincrónica.
            await _context.SaveChangesAsync();

            // Devolvemos una respuesta "CreatedAtAction", indicando que se ha creado correctamente el nuevo rol,
            // y devolvemos la URL del recurso recién creado junto con el objeto creado.
            return CreatedAtAction(nameof(GetRole), new { id = role.RoleID }, role);
        }
        // Acción para actualizar un rol existente (PUT: api/Roles/5)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Class_Role role)
        {
            // Comprobamos que el ID de la URL coincida con el ID del rol en el objeto.
            if (id != role.RoleID)
            {
                return BadRequest(); // Si no coinciden, devolvemos un error "BadRequest".
            }

            // Establecemos el estado de la entidad como "Modificada" para que se guarden los cambios en la base de datos.
            _context.Entry(role).State = EntityState.Modified;

            try
            {
                // Intentamos guardar los cambios en la base de datos de manera asincrónica.
                await _context.SaveChangesAsync();
            }
            // Si ocurre un error de concurrencia en la actualización, manejamos la excepción.
            catch (DbUpdateConcurrencyException)
            {
                // Si el rol ya no existe en la base de datos, devolvemos "NotFound".
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    // Si la excepción es diferente, la volvemos a lanzar.
                    throw;
                }
            }

            // Si la actualización se realiza correctamente, devolvemos "NoContent" (sin contenido).
            return NoContent();
        }
        // Acción para eliminar un rol por ID (DELETE: api/Roles/5)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            // Buscamos el rol en la base de datos usando el ID.
            var role = await _context.tb_Roles.FindAsync(id);

            // Si no se encuentra el rol, devolvemos "NotFound".
            if (role == null)
            {
                return NotFound();
            }

            // Si el rol existe, lo eliminamos de la base de datos.
            _context.tb_Roles.Remove(role);

            // Guardamos los cambios en la base de datos de manera asincrónica.
            await _context.SaveChangesAsync();

            // Devolvemos "NoContent" indicando que la operación de eliminación fue exitosa.
            return NoContent();
        }
        // Método auxiliar para verificar si un rol existe por ID.
        private bool RoleExists(int id)
        {
            // Comprobamos si existe un rol con el ID especificado en la base de datos.
            return _context.tb_Roles.Any(e => e.RoleID == id);
        }
    }//Llave de la clase
}// Llave del namespace
