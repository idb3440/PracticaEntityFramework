using Data;
using Data.Repositories;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RecetaCtrl
    {
        private readonly RecetaRepository _recetaRepository;
        private readonly CategoriaRepository _categoriaRepository;
        private readonly CalificacionRepository _calificacionRepository;

        //public RecetaCtrl(EFContext cntx)
        //{
        //    this._recetaRepository = new RecetaRepository(cntx);
        //    this._categoriaRepository = new CategoriaRepository(cntx);
        //    this._calificacionRepository = new CalificacionRepository(cntx);
        //}
        public RecetaCtrl(RecetaRepository recetarepository, CategoriaRepository categoriarepository, CalificacionRepository calificacionrepository)
        {
            this._recetaRepository = recetarepository;
            this._categoriaRepository = categoriarepository;
            this._calificacionRepository = calificacionrepository;
        }

        public List<Receta> GetReceta(int? id = null, int? idUsuario = null, string nombre = "",string ingredientes = "", List<int> idsCategorias = null)
        {
            return this._recetaRepository.Get(id, idUsuario, nombre, ingredientes, idsCategorias).OrderBy(p => p.IdReceta).ToList();
        }
        
        public Receta GetRecetaById(int idR)
        {
            var result = this._recetaRepository.Get(idR,null,"","",null);
            if (result.Count != 1)
                return null;
            return result.First();
        }

        public Receta InsertReceta(int[] idsCategoria,int idUsuario, string nombre, string descripcion, string ingredientes, string preparacion, byte[] imagen)
        {
            var list = new List<Categoria>();
            foreach (var id in idsCategoria) 
            {
                var CategoriasAlmacenadas = this._categoriaRepository.Get(id);
                Categoria catego = new Categoria();
                catego = CategoriasAlmacenadas.First();
                list.Add(catego);
            }

            var entity = new Receta
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Ingredientes = ingredientes,
                PasosPreparacion = preparacion,
                Imagen = imagen,
                Visible = true,
                IdUsuario = idUsuario,
                Categorias = list
            };

            this._recetaRepository.Insert(entity);

            return entity;
        }
        public Receta UpdateReceta(int[] idsCategoria, int idReceta, string nombre, string descripcion, string ingredientes, string preparacion, byte[] imagen)
        {
            var list = new List<Categoria>();
            foreach (var id in idsCategoria)
            {
                var CategoriasAlmacenadas = this._categoriaRepository.Get(id);

                Categoria catego = new Categoria();
                catego = CategoriasAlmacenadas.First();

                list.Add(catego);
            }

            var entity = new Receta
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Ingredientes = ingredientes,
                PasosPreparacion = preparacion,
                Imagen = imagen,
                Categorias = list
            };

            this._recetaRepository.Update(entity, idReceta);

            return entity;
        }
        public void HideReceta(int id)
        {
            this._recetaRepository.Hide(id);
        }
        public void DeleteReceta(int id)
        { 
            //LAS CALIFICACIONES SE ELIMINAN EN CASCADA
            this._recetaRepository.Delete(id);          
        }


        /****** S E R V I C I O   D E   C A L I F I C A C I O N ******/

        public List<Calificacion> GetCalificaciones(int? id = null, int? idUsuario = null, int? idReceta = null, DateTime? desde = null, DateTime? hasta = null)
        {
            return this._calificacionRepository.Get(id, idReceta,idUsuario, desde, hasta).OrderBy(p => p.IdReceta).ToList();
        }
        public Calificacion GetCalificacionById(int id)
        {
            var result = this._calificacionRepository.Get(id, null, null, null, null);
            if (result.Count != 1)
                return null;
            return result.First();
        }

        public Calificacion InsertCalificacion(int idReceta, int idUsuario, int puntos)
        {
            var entity = new Calificacion
            {
                IdReceta = idReceta,
                IdUsuario = idUsuario,
                Puntuacion = puntos,
                Fecha = DateTime.Now
            };

            this._calificacionRepository.Insert(entity);

            return entity;
        }
        public Calificacion UpdateCalificacion(int idCalificacion, int puntos)
        {
            var entity = new Calificacion
            {
                Puntuacion = puntos,
                Fecha = DateTime.Now
            };
            this._calificacionRepository.Update(entity, idCalificacion);

            return entity;
        }
        public Receta DeleteCalificacion(int idCalificacion)
        {
            try
            {
                this._calificacionRepository.Delete(idCalificacion);
            }
            catch (ArgumentNullException nullex)
            {
                //NO EXISTE LA CALIFICACIÓN QUE SE QUIERE ELIMINAR
                throw new ArgumentException(nullex.Message);
            }
            return null;
            
        }

    }
}
