using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleverCore.Infrastructure.SharedKernel;

namespace CleverCore.Data.Entities
{
    [Table("Sizes")]
    public class Size : DomainEntity<int>
    {

        [MaxLength(250)]
        public string Name
        {
            get; set;
        }
    }
}
