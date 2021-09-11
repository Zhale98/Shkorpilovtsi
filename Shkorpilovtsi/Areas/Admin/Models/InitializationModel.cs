using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Areas.Admin.Models
{
    public class InitializationModel
    {
        [Display(Name = "е-мейл")]
        public string Email { get; set; }
        [Display(Name = "парола")]        
        public string Password { get; set; }        
    }
}
