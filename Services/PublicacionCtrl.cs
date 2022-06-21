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
    public class PublicacionCtrl
    {
        private readonly PublicacionRepository _repository;
        private readonly ComentarioRepository _comentarioRepository;

        //public PublicacionCtrl(EFContext cntx)
        //{
        //    this._repository = new PublicacionRepository(cntx);
        //    this._comentarioRepository = new ComentarioRepository(cntx);
        //}

        public PublicacionCtrl(PublicacionRepository publicacionrepository, ComentarioRepository comentariorepository)
        {
            this._repository = publicacionrepository;
            this._comentarioRepository = comentariorepository;
        }

        public List<Publicacion> GetPublicacion(int? id = null, int? idUsuario = null, int? idReceta = null, string titulo = "", DateTime? desde = null, DateTime? hasta = null)
        {
            return this._repository.Get(id, idUsuario, idReceta, titulo, desde, hasta).OrderBy(p => p.IdPublicacion).ToList();
        }
        public Publicacion GetPublicacionById(int id)
        {
            var result = this._repository.Get(id,null, null, "", null, null);
            if (result.Count != 1)
                return null;
            return result.First();
        }
        public Publicacion InsertPublicacion(string titulo, int idReceta)
        {

            var entity = new Publicacion
            {
                Titulo = titulo,
                Fecha = DateTime.Now,
                Visible = true,
                IdReceta = idReceta
            };

            this._repository.Insert(entity);

            return entity;
        }
        public Publicacion UpdatePublicacion(int id, string titulo)
        {
            var entity = new Publicacion
            {
                Titulo = titulo
            };

            this._repository.Update(entity, id);

            return entity;
        }
        public void HidePublicacion(int id)
        {
            var entity = new Publicacion
            {
                Visible = false
            };

            this._repository.Hide(entity, id);
        }
        public void DeletePublicacion(int idPublicacion)
        {
            //LOS COMENTARIOS SE ELIMINAN EN CASCADA
            this._repository.Delete(idPublicacion);
        } 

        public List<Publicacion> GetPublicacionesUnUsuario(List<Receta> listRecetas) {

            var listPublicaciones = GetPublicacion();
            var listFinal = new List<Publicacion>();

            foreach (Receta rec in listRecetas)
            {
                foreach (Publicacion pub in listPublicaciones)
                {
                    if (rec.IdReceta == pub.IdReceta)
                        listFinal.Add(pub);
                }
            }

            return listFinal;
        }


        /****** S E R V I C I O   D E   C O M E N T A R I O ******/

        public List<Comentario> GetComentario(int? id = null, int? idPublicacion = null, int? idUsuario = null, DateTime? desde = null, DateTime? hasta = null)
        {
            return this._comentarioRepository.Get(id, idPublicacion, idUsuario, desde, hasta).OrderBy(p => p.IdComentario).ToList();
        }
        public Comentario GetByIdComentario(int id)
        {
            var result = this._comentarioRepository.Get(id, null, null, null, null);
            if (result.Count != 1)
                return null;
            return result.First();
        }
        public Comentario InsertComentario(string contenido, int idUsuario, int idPub)
        {
            var entity = new Comentario
            {
                Contenido = contenido,
                Fecha = DateTime.Now,
                IdUsuario = idUsuario,
                IdPublicacion = idPub
            };

            this._comentarioRepository.Insert(entity);

            return entity;
        }
        public Comentario UpdateComentario(int id, string contenido, int idUsuario, int idPublicacion)
        {
            var entity = new Comentario
            {
                Contenido = contenido,
                Fecha = DateTime.Now,
                IdUsuario = idUsuario,
                IdPublicacion = idPublicacion
            };

            this._comentarioRepository.Update(entity, id);

            return entity;
        }
        public void DeleteComentario(int id)
        {
            this._comentarioRepository.Delete(id);
        }
    }
}
