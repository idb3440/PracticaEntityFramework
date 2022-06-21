using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdUsuario { get; set; }

        [Required]
        [StringLength(10)]
        public string Nickname { get; set; }

        //PROPIEDADES QUE EL OTRO EQUIPO DECIDA

        public virtual List<Receta> Recetas { get; set; }
    }
}
