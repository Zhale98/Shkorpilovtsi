using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public int CategoryId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "ЦЕНА ЗА ДЕН")]
        public decimal SingleDayPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "ЦЕНА ЗА ЦЯЛАТА СМЯНА")]
        public decimal FullPrice { get; set; }
        [ForeignKey(nameof(ShiftId))]
        public virtual Shift Shift { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
