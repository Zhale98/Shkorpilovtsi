using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shkorpilovtsi.Models
{
    public class RezervationDetail
    {
        [Key]
        public int Id { get; set; }
        public int ReservationId {  get; set; }
        public int? CategoryId { get; set; }
        public int? SpecialFeeId { get; set; }
        public string Description { get; set; }        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
