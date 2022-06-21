using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Receta 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdReceta { get; set; }
        public byte[] Imagen { get; set; }
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(200)]
        public string Descripcion{ get; set; }

        [Required]
        [StringLength(250)]
        public string Ingredientes { get; set; }
        [Required]
        [StringLength(250)]
        public string PasosPreparacion { get; set; }
        [Required]
        public bool Visible { get; set; }

        //FK
        [Required]
        public int IdUsuario { get; set; }
        public virtual List<Categoria> Categorias { get; set; }

        public virtual List<Calificacion> Calificaciones { get; set; }

        [NotMapped]
        public double? Promedio {
            get 
            {
                if (this.Calificaciones == null)
                    return 0;

                if (this.Calificaciones.Count > 0)
                    return this.Calificaciones.Average(x => x.Puntuacion);

                return 0;
            }
        
        }

        
    }
}
