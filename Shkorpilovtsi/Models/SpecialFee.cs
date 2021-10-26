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
        public int? ShiftId { get; set; }
        public int? CategoryId { get; set; }
        [Display(Name = "НАИМЕНОВАНИЕ")]
        public string Name { get; set; }
        [Display(Name = "ЦЕНА ЗА ДЕН")]
        [Column(TypeName = "decimal(18,2)")]        
        public decimal SingleDayPrice { get; set; }
        [Display(Name = "ЦЕНА ЗА ЦЯЛАТА СМЯНА")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FullPrice { get; set; }
    }
}
