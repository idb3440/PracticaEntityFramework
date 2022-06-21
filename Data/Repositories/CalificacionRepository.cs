using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CalificacionRepository
    {
        private EFContext _context;

        public CalificacionRepository()
        {
            this._context = new EFContext();
        }
        public CalificacionRepository(EFContext context)
        {
            this._context = context;
        }
        
        public List<Calificacion> Get(int? id,int? idReceta, int? idPerfil)
        {
            return this._context.Calificaciones.Where(c => (id == null || c.IdCalificacion == id)
            && (idReceta == null || c.IdReceta == idReceta)
            && (idPerfil == null || c.IdPerfil == idPerfil)).ToList();
        }
        public void Insert(Calificacion elemento)
        {
            this._context.Calificaciones.Add(elemento);
            this._context.SaveChanges();
        }
        public void Update(Calificacion elemento, int idCalificacion)
        {
            var c = this._context.Calificaciones.Find(idCalificacion);
            var a = this._context.Calificaciones.Attach(c);

            a.Nota = elemento.Nota; //c.Nota
            a.Fecha = elemento.Fecha;

            //this._context.Entry(c).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();
        }
        public void Delete(int id)
        {
            var d = this._context.Calificaciones.Find(id);
            this._context.Calificaciones.Remove(d);
            this._context.SaveChanges();
        }

    }
}
