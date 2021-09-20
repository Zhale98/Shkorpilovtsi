using System;
using System.ComponentModel.DataAnnotations;

namespace Shkorpilovtsi.Models
{
    public class Rezervation
    {
        [Key]
        public int Id {  get; set; }
        public string UserId { get; set; }
        public int BungalowId { get; set; }
        public int ShiftId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Approved { get; set; }
        public bool Cancelled { get; set; }
    }
}
