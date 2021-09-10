using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class Bungalow
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "НОМЕР НА БУНАЛО")]
        public int Number { get; set; }
        [Display(Name = "ОПИСАНИЕ НА БУНАЛО")]
        public string Description { get; set; }
        [Display(Name = "СВОБОДНО")]
        public bool IsAvailable { get; set; }
        [Display(Name = "АКТИВНО")]
        public bool IsActive { get; set; }
        [Display(Name = "КООРДИНАТИ НА КАРТАТА")]
        public string MapCoords { get; set; }        
    }
}
