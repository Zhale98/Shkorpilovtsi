using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class SpecialFee
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "НАИМЕНОВАНИЕ")]
        public string Name { get; set; }
        [Display(Name = "ЦЕНА")]
        [Column(TypeName = "decimal(18,2)")]        
        public decimal Price { get; set; }
    }
}
