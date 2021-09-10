using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class Bed
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Капацитет")]
        [Column(TypeName = "decimal(18,1)")]
        public decimal Capacity { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
