using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EFContext : DbContext
    {
        public EFContext() : base("name=CadenaConexionEF")
        {
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Perfil> Perfiles { get; set; }
        public virtual DbSet<Receta> Recetas { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Publicacion> Publicaciones { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Calificacion> Calificaciones { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Perfil>().ToTable("Perfiles");
            modelBuilder.Entity<Receta>().ToTable("Recetas");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Publicacion>().ToTable("Publicaciones");
            modelBuilder.Entity<Comentario>().ToTable("Comentarios");
            modelBuilder.Entity<Calificacion>().ToTable("Calificaciones");
            
            modelBuilder.Entity<Receta>().HasMany(t => t.Categorias).WithMany()
                .Map(m => { 
                    m.ToTable("RecetasCategorias"); 
                    m.MapLeftKey("IdReceta"); 
                    m.MapRightKey("IdCategoria"); 
                });

            /********************** R E L A C I O N E S   E N T R E   T A B L A S **********************/

            modelBuilder.Entity<Perfil>().HasRequired(x => x.Usuario).WithMany()
                .HasForeignKey(x => x.IdUsuario);
            
            modelBuilder.Entity<Receta>().HasRequired(x => x.Perfil).WithMany()
                .HasForeignKey(x => x.IdPerfil).WillCascadeOnDelete(false);

            modelBuilder.Entity<Publicacion>().HasRequired(x => x.Receta).WithMany()
                .HasForeignKey(x => x.IdReceta).WillCascadeOnDelete(false);

            modelBuilder.Entity<Comentario>().HasRequired(x => x.Publicacion).WithMany().HasForeignKey(x => x.IdPublicacion);
            modelBuilder.Entity<Comentario>().HasRequired(x => x.Perfil).WithMany().HasForeignKey(x => x.IdPerfil);

            modelBuilder.Entity<Calificacion>().HasRequired(x => x.Perfil).WithMany().HasForeignKey(x=>x.IdPerfil);
            //modelBuilder.Entity<Calificacion>().HasRequired(x => x.Receta).WithMany().HasForeignKey(x=>x.IdReceta);
             
        }
    }
}
