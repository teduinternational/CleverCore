using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleverCore.Data.Enums;
using CleverCore.Data.Interfaces;
using CleverCore.Infrastructure.SharedKernel;

namespace CleverCore.Data.Entities
{
    [Table("Feedbacks")]
    public class Feedback : DomainEntity<int>,ISwitchable, IDateTracking
    {
       
        [MaxLength(250)]
        [Required]
        public string Name { set; get; }

        [MaxLength(250)]
        public string Email { set; get; }

        [MaxLength(500)]
        public string Message { set; get; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
    }
}
