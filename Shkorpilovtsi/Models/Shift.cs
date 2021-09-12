using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "НОМЕР НА СМЯНА")]
        public string Number { get; set; }
        [Display(Name = "НАЧАЛНА ДАТА")]
        public DateTime StartDate { get; set; }
        [Display(Name = "КРАЙНА ДАТА")]
        public DateTime EndDate { get; set; }
    }
}
