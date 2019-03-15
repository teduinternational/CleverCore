using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CleverCore.Data.Entities
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>
    {
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
