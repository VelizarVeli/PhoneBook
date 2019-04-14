using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class PhoneUser : BaseId
    {
        [Required]
        public string Name { get; set; }

		[Required]
        public string PhoneNumber { get; set; }

        public virtual ICollection<OutgoingCall> OutgoingCalls { get; set; } = new HashSet<OutgoingCall>();
    }
}
