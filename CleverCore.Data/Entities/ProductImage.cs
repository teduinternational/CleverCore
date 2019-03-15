using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleverCore.Infrastructure.SharedKernel;

namespace CleverCore.Data.Entities
{
    [Table("ProductImages")]
    public class ProductImage : DomainEntity<int>
    {
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [MaxLength(250)]
        public string Path { get; set; }

        [MaxLength(250)]
        public string Caption { get; set; }
    }
}
