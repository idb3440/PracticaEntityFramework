using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Repositories;
using Domain.Entidades;
using Services;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio");

            using (var cntx = new EFContext())//crear variable que se compartira para tener la misma instancia
            {
                {
                    //var user = new Usuario();
                    //user.Nickname = "Usuario A";
                    //cntx.Usuarios.Add(user);
                    //cntx.SaveChanges();
                }
                
                RecetaRepository _recetaRepository = new RecetaRepository(cntx);
                CategoriaRepository _categoriaRepository = new CategoriaRepository(cntx);
                CalificacionRepository _calificacionRepository = new CalificacionRepository(cntx);
                PublicacionRepository _publicacionRepository = new PublicacionRepository(cntx);
                ComentarioRepository _comentarioRepository = new ComentarioRepository(cntx);

                MenuPrincipal(_recetaRepository, _categoriaRepository, _calificacionRepository, _publicacionRepository, _comentarioRepository);
            }

            Console.WriteLine("Fin");
            Console.ReadKey();
        }

        public static void MenuPrincipal(RecetaRepository _recetaRepository, CategoriaRepository _categoriaRepository, CalificacionRepository _calificacionRepository, PublicacionRepository _publicacionRepository, ComentarioRepository _comentarioRepository) 
        {
            Console.WriteLine("MENU");
            Console.WriteLine("1.- Recetas");
            Console.WriteLine("2.- Calificaciones");
            Console.WriteLine("3.- Publicaciones");
            Console.WriteLine("4.- Comentarios");

            int opcion = Int32.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    RecetaMenu(_recetaRepository,_categoriaRepository,_calificacionRepository, _publicacionRepository, _comentarioRepository);
                    break;
                case 2:
                    CalificacionMenu(_recetaRepository, _categoriaRepository, _calificacionRepository, _publicacionRepository, _comentarioRepository);
                    break;
                case 3:
                    PublicacionMenu(_recetaRepository, _categoriaRepository, _calificacionRepository, _publicacionRepository, _comentarioRepository);
                    break;
                case 4:
                    ComentarioMenu(_recetaRepository, _categoriaRepository, _calificacionRepository, _publicacionRepository, _comentarioRepository);
                    break;
                default:
                    break;
            }
        }
        

        public static void RecetaMenu(RecetaRepository rr, CategoriaRepository cr,  CalificacionRepository cfr, PublicacionRepository pr, ComentarioRepository ctr) 
        {
            Console.WriteLine("-- Menú Recetas --");
            Console.WriteLine("     1. Nueva Receta");
            Console.WriteLine("     2. Editar Receta");
            Console.WriteLine("     3. Ocultar/No Ocultar Receta");
            Console.WriteLine("     4. Ver UNA Receta");
            Console.WriteLine("     5. Ver TODAS las Recetas");
            Console.WriteLine("     6. Ver las Recetas de UN Usuario");
            Console.WriteLine("     7. Ver las Recetas de UNA Categoria");
            Console.WriteLine("     8. Ver las Recetas con cierto INGREDIENTE");
            Console.WriteLine("x. Volver");

            int opcion = Int32.Parse(Console.ReadLine());
            var RecetaCtrl = new RecetaCtrl(rr, cr, cfr);
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("C- RECETAS");
                    int[] idsCategoria = new int[] { 1, 2 };
                    RecetaCtrl.InsertReceta(idsCategoria, 2, "Cereal con leche", "Desayuno de campeones", "-Cereal, -Leche", "En un plato servir el cereal y seguidamente la leche", null);
                    RecetaMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 2:
                    Console.WriteLine("U- RECETA 1");
                    int[] idsCategoria2 = new int[] { 3 };
                    RecetaCtrl.UpdateReceta(idsCategoria2,1, "Licuado choco-manzana", "breve descripción", "-1 1/2 Tazas de Leche, -1 Manzana con cáscara, -2 Cdas de chocomilk, -1/2 Taza de Avena", "Licuar todos los ingredientes y servir con hielo y rodajas de manzana", null);
                    RecetaMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 3:
                    Console.WriteLine("Hide- RECETA");
                    RecetaCtrl.HideReceta(2);
                    RecetaMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 5:
                    Console.WriteLine("R- RECETAS");
                    var listado = RecetaCtrl.GetReceta();
                    foreach (var el in listado)
                    {
                        Console.WriteLine(string.Format("{0}.- NOMBRE: {1}\n INGREDIENTES: {2}\n PREPARACIÓN:{3} \n CALIFICACIÓN ROMEDIO: {4}", el.IdReceta, el.Nombre, el.Ingredientes, el.PasosPreparacion, el.Promedio));
                    }
                    RecetaMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 4:
                    Console.WriteLine("R- *UNA* RECETA ");
                    var s = RecetaCtrl.GetRecetaById(1);
                    Console.WriteLine(string.Format("{0}.- NOMBRE: {1}\n INGREDIENTES: {2}\n PREPARACIÓN:{3} \n CALIFICACIÓN ROMEDIO: {4}", s.IdReceta, s.Nombre, s.Ingredientes, s.PasosPreparacion, s.Promedio));
                    RecetaMenu(rr, cr, cfr, pr, ctr);
                    break; 
                case 6:
                    Console.WriteLine("R- RECETAS DE *UN* Usuario");
                    var listA = RecetaCtrl.GetReceta(null,2);
                    foreach (var el in listA)
                    {
                        Console.WriteLine(string.Format("{0}.- NOMBRE: {1}\n INGREDIENTES: {2}\n PREPARACIÓN:{3} \n CALIFICACIÓN ROMEDIO: {4}", el.IdReceta, el.Nombre, el.Ingredientes, el.PasosPreparacion, el.Promedio));
                    }
                    RecetaMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 7:
                    Console.WriteLine("R- RECETAS QUE SON PARTE DE *UNA* CATEGORIA");
                    var ids = new List<int> { 4 };
                    var listsRecetasCategoriaBuscada = RecetaCtrl.GetReceta(null, null, "", "", ids);
                    foreach (var el in listsRecetasCategoriaBuscada)
                    {
                        Console.WriteLine(string.Format("{0}.- NOMBRE: {1}\n INGREDIENTES: {2}\n PREPARACIÓN:{3} \n CALIFICACIÓN ROMEDIO: {4}", el.IdReceta, el.Nombre, el.Ingredientes, el.PasosPreparacion, el.Promedio));
                    }
                    RecetaMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 8:
                    Console.WriteLine("R- RECETAS QUE CONTIENEN CIERTO INGREDIENTE");
                    var list = RecetaCtrl.GetReceta(null, null, null, "Leche");
                    foreach (var el in list)
                    {
                        Console.WriteLine(string.Format("{0}.- NOMBRE: {1}\n INGREDIENTES: {2}\n PREPARACIÓN:{3} \n CALIFICACIÓN ROMEDIO: {4}", el.IdReceta, el.Nombre, el.Ingredientes, el.PasosPreparacion, el.Promedio));
                    }
                    RecetaMenu(rr, cr, cfr, pr, ctr);
                    break;
                default:
                    break;
            }
            MenuPrincipal(rr, cr, cfr, pr, ctr);
        }
        public static void CalificacionMenu(RecetaRepository rr, CategoriaRepository cr, CalificacionRepository cfr, PublicacionRepository pr, ComentarioRepository ctr)
        {
            Console.WriteLine("-- Menú Calificaciones --");
            Console.WriteLine("     1. Nueva Calificación");
            Console.WriteLine("     2. Editar Calificación");
            Console.WriteLine("     3. Borrar Calificación");
            Console.WriteLine("     4. Ver TODAS las Calificaciones");
            Console.WriteLine("     5. Ver TODAS las Calificaciones de UNA Receta");
            Console.WriteLine("     6. Ver TODAS las Calificaciones que ha dado UN Usuario");
            Console.WriteLine("     7. Ver UNA Calificación");
            Console.WriteLine("x. Volver");

            int opcion = Int32.Parse(Console.ReadLine());
            var RecetaCtrl = new RecetaCtrl(rr, cr, cfr);

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("C- CALIFICACIÓN");
                    RecetaCtrl.InsertCalificacion(5, 1, 2);
                    CalificacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 2:
                    Console.WriteLine("U- CALIFICACIÓN");
                    RecetaCtrl.UpdateCalificacion(4, 9);
                    CalificacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 3:
                    Console.WriteLine("D- CALIFICACIÓN");
                    RecetaCtrl.DeleteCalificacion(1);
                    CalificacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 4:
                    Console.WriteLine("R- CALIFICACIONES");
                    var lista = RecetaCtrl.GetCalificaciones();
                    foreach (var el in lista)
                    {
                        Console.WriteLine(string.Format("ID_CALIFICACION: {0} FECHA: {1} \n  ID_Usuario: {2} Receta: {3} **Puntos:{4}", el.IdCalificacion, el.Fecha, el.IdUsuario, el.IdReceta, el.Puntuacion));
                    }
                    CalificacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 5:
                    Console.WriteLine("R- CALIFICACIONES DE *UNA* RECETA");
                    var idRec = 1;
                    var lista2 = RecetaCtrl.GetCalificaciones(null, null, idRec);
                    foreach (var el in lista2)
                    {
                        Console.WriteLine(string.Format("ID_CALIFICACION: {0} FECHA: {1} \n  ID_Usuario: {2} Receta: {3} **Puntos:{4}", el.IdCalificacion, el.Fecha, el.IdUsuario, el.IdReceta, el.Puntuacion));
                    }
                    CalificacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 6:
                    Console.WriteLine("R- CALIFICACIONES DE *UN* Usuario");
                    var lista3 = RecetaCtrl.GetCalificaciones(null, 2);
                    foreach (var el in lista3)
                    {
                        Console.WriteLine(string.Format("ID_CALIFICACION: {0} FECHA: {1} \n  ID_Usuario: {2} Receta: {3} **Puntos:{4}", el.IdCalificacion, el.Fecha, el.IdUsuario, el.IdReceta, el.Puntuacion));
                    }
                    CalificacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 7:
                    Console.WriteLine("R- UNA CALIFICACIÓN");
                    var registro = RecetaCtrl.GetCalificacionById(3);
                    Console.WriteLine(string.Format("ID_CALIFICACION: {0} FECHA: {1} \n  ID_Usuario: {2} Receta: {3} **Puntos:{4}", registro.IdCalificacion, registro.Fecha, registro.IdUsuario, registro.IdReceta, registro.Puntuacion));
                    CalificacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                default:
                    break;
            }
            MenuPrincipal(rr, cr, cfr, pr, ctr);
        }
        public static void PublicacionMenu(RecetaRepository rr, CategoriaRepository cr, CalificacionRepository cfr, PublicacionRepository pr, ComentarioRepository ctr) 
        {
            Console.WriteLine("-- Menú Publicaciones --");
            Console.WriteLine("     1. Nueva Publicación");
            Console.WriteLine("     2. Editar Publicación");
            Console.WriteLine("     3. Ocultar Publicación");
            Console.WriteLine("     4. Borrar Publicación");
            Console.WriteLine("     5. Ver TODAS las Publicaciones");
            Console.WriteLine("     6. Ver UNA Publicación");
            Console.WriteLine("     7. Ver TODAS las Publicaciones de UN Usuario");
            Console.WriteLine("     8. Ver TODAS las Publicaciones hechas entre FECHAS");
            Console.WriteLine("x. Volver");

            int opcion = Int32.Parse(Console.ReadLine());
            var PublCtrl = new PublicacionCtrl(pr, ctr);

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("C- Publicación");
                    PublCtrl.InsertPublicacion("Macarron para la familia", 3);
                    PublicacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 2:
                    Console.WriteLine("U- Publicación");
                    PublCtrl.UpdatePublicacion(2, "Licuado - Edicion");
                    PublicacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 3:
                    Console.WriteLine("Hide- Publicación");
                    PublCtrl.HidePublicacion(2);
                    PublicacionMenu(rr, cr, cfr, pr, ctr);
                    break; 
                case 4:
                    Console.WriteLine("D- Publicación");
                    PublCtrl.DeletePublicacion(3);
                    PublicacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 5:
                    Console.WriteLine("R- Publicaciones");
                    var lista = PublCtrl.GetPublicacion();
                    foreach (var el in lista)
                    {
                        Console.WriteLine(string.Format("{0}.- {1}  \n FECHA: {2} ID_RECETA: {3}", el.IdPublicacion, el.Titulo, el.Fecha, el.IdReceta));
                    }
                    PublicacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 6:
                    Console.WriteLine("R- UNA Publicación");
                    var elemento = PublCtrl.GetPublicacionById(2);
                    Console.WriteLine(string.Format("{0}.- {1}  \n FECHA: {2} ID_RECETA: {3}", elemento.IdPublicacion, elemento.Titulo, elemento.Fecha, elemento.IdReceta));
                    PublicacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 7:
                    Console.WriteLine("R- TODAS las Publicaciones de UN Usuario");
                    var idUsuario = 1;
                    var listPublicaciones = PublCtrl.GetPublicacion(null,idUsuario);
                    foreach (var el in listPublicaciones)
                    {
                        Console.WriteLine(string.Format("{0}.- {1}  \n FECHA: {2} ID_RECETA: {3}", el.IdPublicacion, el.Titulo, el.Fecha, el.IdReceta));
                    }
                    PublicacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 8:
                    Console.WriteLine("R- Publicaciones que se hicieron desde el 2022-02-12 hasta hoy");
                    var desde = Convert.ToDateTime("2022-02-12");
                    var list = PublCtrl.GetPublicacion(null,null,null,"",desde);
                    foreach (var el in list)
                    {
                        Console.WriteLine(string.Format("{0}.- {1}  \n FECHA: {2} ID_RECETA: {3}", el.IdPublicacion, el.Titulo, el.Fecha, el.IdReceta));
                    }

                    Console.WriteLine("R- Publicaciones que se hicieron hasta el 2022-02-14");
                    list = PublCtrl.GetPublicacion(null, null, null, "",null, Convert.ToDateTime("2022-02-14"));
                    foreach (var el in list)
                    {
                        Console.WriteLine(string.Format("{0}.- {1}  \n FECHA: {2} ID_RECETA: {3}", el.IdPublicacion, el.Titulo, el.Fecha, el.IdReceta));
                    }
                    
                    PublicacionMenu(rr, cr, cfr, pr, ctr);
                    break;
                default:
                    break;
            }
            MenuPrincipal(rr, cr, cfr, pr, ctr);
        }
        public static void ComentarioMenu(RecetaRepository rr, CategoriaRepository cr, CalificacionRepository cfr, PublicacionRepository pr, ComentarioRepository ctr) 
        {
            Console.WriteLine("-- Menú Comentarios --");
            Console.WriteLine("     1. Nuevo Comentario");
            Console.WriteLine("     2. Editar Comentario");
            Console.WriteLine("     3. Borrar Comentario");
            Console.WriteLine("     4. Ver TODOS los Comentarios");
            Console.WriteLine("     5. Ver los comentarios de UNA Publicación");
            Console.WriteLine("x. Volver");

            int opcion = Int32.Parse(Console.ReadLine());
            var comentCtrl = new PublicacionCtrl(pr, ctr);

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("C- Comentario");
                    comentCtrl.InsertComentario("Mi Comentario para la publicación 4",1, 4);
                    ComentarioMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 2:
                    Console.WriteLine("U- Comentarios");
                    comentCtrl.UpdateComentario(1, "Mi comentario edit", 1, 1);
                    ComentarioMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 3:
                    Console.WriteLine("D- Comentarios");
                    comentCtrl.DeleteComentario(3);
                    ComentarioMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 4:
                    Console.WriteLine("R- Comentarios");
                    var listado = comentCtrl.GetComentario();
                    foreach (var el in listado)
                    {
                        Console.WriteLine(string.Format("Pubicación {0} => {1}.- {2}", el.IdPublicacion, el.IdComentario, el.Contenido));
                    }
                    ComentarioMenu(rr, cr, cfr, pr, ctr);
                    break;
                case 5:
                    Console.WriteLine("R- Comentarios de UNA publicación");
                    var aPublicacion = comentCtrl.GetPublicacionById(1); //Obtengo la publicación
                    var listComentarios = aPublicacion.Comentarios; //obtengo el listado de comentarios de esa publicación
                    foreach (var el in listComentarios)
                    {
                        Console.WriteLine(string.Format("{0}.- {1}", el.IdComentario, el.Contenido));
                    }
                    ComentarioMenu(rr, cr, cfr, pr, ctr);
                    break;
                default:
                    break;
            }
            MenuPrincipal(rr, cr, cfr, pr, ctr);
        }
    }
}
