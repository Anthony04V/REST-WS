
namespace Servicio_REST_CRUD_SQL.Models
{
    public class Class_Role
    {
        public int RoleID { get; set; }             // ID autoincremental
        public string RoleName { get; set; }        // Nombre del rol, requerido
        public string Description { get; set; }     // Descripción del rol, opcional
        public DateTime F_Created { get; set; }     // Fecha de creación
        public DateTime? F_Updated { get; set; }    // Fecha de actualización, opcional
        public bool IsActive { get; set; } = true;  // Rol activo o inactivo
                                                    // predeterminado es activo (true)
    }// Llave de la clase
}// namespace

