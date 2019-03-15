using System.ComponentModel.DataAnnotations.Schema;
using CleverCore.Infrastructure.SharedKernel;

namespace CleverCore.Data.Entities
{
    [Table("BillDetails")]
    public class BillDetail : DomainEntity<int>
    {
        public int BillId { set; get; }

        public int ProductId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        [ForeignKey("ColorId")]
        public virtual Color Color { set; get; }

        [ForeignKey("SizeId")]
        public virtual Size Size { set; get; }
    }
}
