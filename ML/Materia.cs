using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-ZñÑ\\s]{1,50}$")]
        public string Nombre { get; set; }
        [Required]
        [RegularExpression("^\\d+(\\.\\d+)?$")]
        public decimal Costo { get; set; }
        public List<object> Materias { get; set; }
    }
}
