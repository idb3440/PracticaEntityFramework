using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PublicacionRepository
    {
        private EFContext _context;

        public PublicacionRepository()
        {
            this._context = new EFContext();
        }

        public PublicacionRepository(EFContext context)
        {
            this._context = context;
        }

        public List<Publicacion> Get(int? id, int? idReceta, string titulo, DateTime? desde, DateTime? hasta)
        {
            return this._context.Publicaciones.Where(c => (id == null || c.IdPublicacion == id)
            && (idReceta == null || c.IdReceta == idReceta)
            && (titulo == null || c.Titulo.Contains(titulo))
            && (desde == null || c.Fecha >= desde)
            && (hasta == null || c.Fecha <= hasta)
            && (c.Visible == true)).ToList();
        }

        public void Insert(Publicacion elemento)
        {
            this._context.Publicaciones.Add(elemento);
            this._context.SaveChanges();
        }

        public void Update(Publicacion elemento, int id)
        {
            var u = this._context.Publicaciones.Find(id);
            u.Titulo = elemento.Titulo;
            
            this._context.Entry(u).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }

        public void Hide(Publicacion elemento, int id)
        {
            var u = this._context.Publicaciones.Find(id);

            u.Visible = elemento.Visible; 

            this._context.Entry(u).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var d = this._context.Publicaciones.Find(id);
            this._context.Publicaciones.Remove(d);
            this._context.SaveChanges();
        }
    }
}
