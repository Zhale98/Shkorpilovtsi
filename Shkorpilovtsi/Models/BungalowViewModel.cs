using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class BungalowViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public string Coords { get; set; }
        public bool HasFridge { get; set; }
        public bool HasWc { get; set; }
        public bool HasSofa { get; set; }        
        public List<Room> Rooms {  get; set; }
        public List<BedsInRoom> BedsInRooms { get; set; }        
    }
}
