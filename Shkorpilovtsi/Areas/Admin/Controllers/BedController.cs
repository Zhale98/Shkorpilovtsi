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
    [Area("admin")]
    [Route("[area]")]
    public class BedController : DataController<Bed>
    {
        public BedController(IDataService<Bed> service) : base(service) { }        
    }
}
