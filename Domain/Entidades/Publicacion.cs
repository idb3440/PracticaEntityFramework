using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Publicacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdPublicacion { get; set; }
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public bool Visible { get; set; }

        //FK
        public int? IdReceta { get; set; }
        public virtual Receta Receta { get; set; }

        public virtual List<Comentario> Comentarios { get; set; }

    }
}
