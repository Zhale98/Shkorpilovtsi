using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shkorpilovtsi.Models
{
    public class RoomsInBungalow
    {
        [Key]
        public int Id { get; set; }
        public int BungalowId { get; set; }
        public int RoomId { get; set; }
        [ForeignKey(nameof(BungalowId))]
        public virtual Bungalow Bungalow { get; set; }
        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; }
    }
}
