using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class UserCategory
    {
        [Key]
        public int Id {  get; set; }
        public int CategoryId {  get; set; }
        public string UserId { get; set; }        
    }
}
