// Incluimos el espacio de nombres para Entity Framework Core
using Microsoft.EntityFrameworkCore;
// Para usar tipos de datos como DateTime
using System;
// Incluimos el espacio de nombres donde está definida la clase de modelo Class_Role
using Servicio_REST_CRUD_SQL.Models;  

namespace Servicio_REST_CRUD_SQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor que llama a la base (DbContext) y pasa las opciones configuradas
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // Esta propiedad representa la tabla "Roles" en la base de datos
        public DbSet<Class_Role> tb_Roles { get; set; }

        // Este método se usa para realizar configuraciones adicionales como restricciones en las tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuramos la tabla "Roles"
            modelBuilder.Entity<Class_Role>(entity =>
            {
                entity.HasKey(e => e.RoleID);  // Definimos que RoleID es la clave primaria

                entity.Property(e => e.RoleName)
                      .IsRequired()  // Hacemos que RoleName sea obligatorio
                      .HasMaxLength(50);  // Definimos que RoleName no puede tener más de 50 caracteres

                entity.HasIndex(e => e.RoleName)
                      .IsUnique();  // Definimos un índice único para que no existan dos roles con el mismo nombre
            });
        }// Llave de OnModelCreating
    }// Llave de la clase 
} // Llave del namespace
