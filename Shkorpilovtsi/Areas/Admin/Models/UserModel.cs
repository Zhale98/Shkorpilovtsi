using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Areas.Admin.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }        
        public bool IsActive { get; set; }
    }
}
