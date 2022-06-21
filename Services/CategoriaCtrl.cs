using Data;
using Data.Repositories;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoriaCtrl
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaCtrl(EFContext cntx)
        {
            this._categoriaRepository = new CategoriaRepository(cntx);
        }
        
        /****** S E R V I C I O   D E   C A T E G O R I A ******/
        public List<Categoria> GetCategoria(int? id = null, string nombre = "")
        {
            return this._categoriaRepository.Get(id, nombre).OrderBy(p => p.IdCategoria).ToList();
        }

        public Categoria InsertCategoria(string nombre)
        {
            var entity = new Categoria
            {
                Nombre = nombre
            };

            this._categoriaRepository.Insert(entity);

            return entity;
        }

        public Categoria UpdateCategoria(int id, string nombre)
        {
            var entity = new Categoria
            {
                Nombre = nombre
            };

            this._categoriaRepository.Update(id, entity);

            return entity;
        }

        public void DeleteCategoria(int id)
        {
            this._categoriaRepository.Delete(id);
        }
    }
}
