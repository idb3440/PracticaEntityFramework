using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ComentarioRepository
    {
        private EFContext _context;

        public ComentarioRepository()
        {
            this._context = new EFContext();
        }

        public ComentarioRepository(EFContext context)
        {
            this._context = context;
        }

        public List<Comentario> Get(int? id, int? idPublicacion, int? idPerfil, DateTime? desde, DateTime? hasta)
        {
            return this._context.Comentarios.Where(c => (id == null || c.IdComentario == id) 
            && (idPublicacion == null ||c.IdPublicacion == idPublicacion) 
            && (idPerfil == null ||c.IdPerfil == idPerfil) 
            && (desde == null || c.Fecha >= desde)
            && (hasta == null || c.Fecha <= hasta)).ToList();
        }

        public void Insert(Comentario elemento)
        {
            this._context.Comentarios.Add(elemento);
            this._context.SaveChanges();
        }

        public void Update(Comentario elemento, int id)
        {
            var u = this._context.Comentarios.Find(id);

            u.Contenido = elemento.Contenido;

            this._context.Entry(u).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var d = this._context.Comentarios.Find(id);
            this._context.Comentarios.Remove(d);
            this._context.SaveChanges();
        }
    }
}
