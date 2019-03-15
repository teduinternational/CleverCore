using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleverCore.Data.Enums;
using CleverCore.Data.Interfaces;
using CleverCore.Infrastructure.SharedKernel;

namespace CleverCore.Data.Entities
{
    [Table("Functions")]
    public class Function : DomainEntity<string>, ISwitchable, ISortable
    {
       
        [Required]
        [MaxLength(128)]
        public string Name { set; get; }

        [Required]
        [MaxLength(250)]
        public string URL { set; get; }


        [MaxLength(128)]
        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
    }
}
