using System.ComponentModel.DataAnnotations;

namespace APEX_WMS.Models
{
    public enum MovementType
    {
        StockIn = 1,
        StockOut = 2,
        Adjustment = 3,
        Transfer = 4
    }

    public class StockMovement
    {
        [Key]
        public int MovementId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public MovementType MovementType { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [StringLength(100)]
        public string FromLocation { get; set; }

        [StringLength(100)]
        public string ToLocation { get; set; }

        public int? OrderId { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime MovementDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
