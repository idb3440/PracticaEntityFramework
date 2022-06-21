using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Calificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdCalificacion { get; set; }
        [Required]
        public int? IdUsuario { get; set; } //  FK
        [Required] 
        public int? IdReceta { get; set; }  // FK
        [Required]
        public int Puntuacion { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        //public virtual Usuario Usuario { get; set; }
        

    }
}
