using System.ComponentModel.DataAnnotations;

namespace APEX_WMS.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(50)]
        public string SKU { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public int ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
