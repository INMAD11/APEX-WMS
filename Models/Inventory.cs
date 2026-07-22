using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APEX_WMS.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityOnHand { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityReserved { get; set; }

        [NotMapped]
        public int AvailableQuantity => QuantityOnHand - QuantityReserved;

        public DateTime LastStockCheckDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
