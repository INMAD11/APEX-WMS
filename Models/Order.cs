using System.ComponentModel.DataAnnotations;

namespace APEX_WMS.Models
{
    public enum OrderType
    {
        PurchaseOrder = 1,
        SalesOrder = 2
    }

    public enum OrderStatus
    {
        Draft = 1,
        Pending = 2,
        Confirmed = 3,
        Shipped = 4,
        Delivered = 5,
        Cancelled = 6
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        public OrderType OrderType { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public int? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
