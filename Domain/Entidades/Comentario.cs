using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdComentario { get; set; }
        [Required]
        [StringLength(250)]
        public string Contenido { get; set; }
        [Required]
        public DateTime Fecha { get; set; }


        //FK
        [Required]
        public int? IdUsuario { get; set; }

        //FK
        public int? IdPublicacion { get; set; }

    }
}
