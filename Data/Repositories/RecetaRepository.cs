using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RecetaRepository
    {
        private EFContext _context;

        public RecetaRepository()
        {
            this._context = new EFContext();
        }

        public RecetaRepository(EFContext context)
        {
            this._context = context;
        }

        public List<Receta> Get(int? id, int? idPerfil, string nombre, string ingredientes)
        {
            return this._context.Recetas.Where(c => (id == null || c.IdReceta == id) 
            && (idPerfil == null || c.IdPerfil == idPerfil)
            && (nombre == "" || c.Nombre.Contains(nombre))
            && (ingredientes == "" || c.Ingredientes.Contains(ingredientes))
            && (c.Visible == true)).ToList();
        }

        public void Insert(Receta elemento)
        {
            this._context.Recetas.Add(elemento);
            this._context.SaveChanges();
        }

        public void Update(Receta elemento, int id)
        {
            var u = this._context.Recetas.Find(id);

            u.Nombre = elemento.Nombre;
            u.Descripcion = elemento.Descripcion;
            u.Ingredientes = elemento.Ingredientes;
            u.PasosPreparacion = elemento.PasosPreparacion;
            u.Imagen = elemento.Imagen;

            this._context.Entry(u).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }
        public void Hide(int id)
        {
            var u = this._context.Recetas.Find(id);
            var h = this._context.Recetas.Attach(u);

            h.Visible = false;

            this._context.SaveChanges();
        }
        public void Delete(int id)
        {
            var d = this._context.Recetas.Find(id);

            //this._context.Recetas.Remove(d);
            this._context.Entry(d).State = System.Data.Entity.EntityState.Deleted;
            
            this._context.SaveChanges();
        }
    }
}
