using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "НАИМЕНОВАНИЕ НА СТАЯТА")]
        public string Name { get; set; }
        [Display(Name = "ОПИСАНИЕ НА СТАЯТА")]
        public string Description { get; set; }
        [Display(Name = "САНИТАРЕН ВЪЗЕЛ")]
        public bool IsWc { get; set; }
        [Display(Name = "РАЗТЕГАТЕЛЕН ДИВАН")]
        public bool HasSofa { get; set; }
        [Display(Name = "ХЛАДИЛНИК")]
        public bool HasFridge { get; set; }
    }
}
