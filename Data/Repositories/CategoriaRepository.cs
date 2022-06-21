using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoriaRepository
    {
        private EFContext _context;

        public CategoriaRepository()
        {
            this._context = new EFContext();
        }

        public CategoriaRepository(EFContext context)
        {
            this._context = context;
        }

        public List<Categoria> Get(int? id = null, string nombre = "")
        {
            return this._context.Categorias.Where(c => (id == null || c.IdCategoria == id)
            && (nombre == "" || c.Nombre.Contains(nombre))).ToList();
        }
        
        public void Insert(Categoria elemento)
        {
            this._context.Categorias.Add(elemento);
            this._context.SaveChanges();
        }

        public void Update(int id, Categoria elemento)
        {

            var c = this._context.Categorias.Find(id);

            c.Nombre = elemento.Nombre;

            this._context.Entry(c).State = System.Data.Entity.EntityState.Modified;
            this._context.SaveChanges();

        }
    }
}
