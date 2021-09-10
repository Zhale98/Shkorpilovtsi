using Microsoft.AspNetCore.Mvc;
using Shkorpilovtsi.Areas.Admin.Models;
using Shkorpilovtsi.Interfaces;
using Shkorpilovtsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]")]
    public class RoomController : DataController<Room>
    {        
        public RoomController(IDataService<Room> service) : base(service) { }        
    }
}
