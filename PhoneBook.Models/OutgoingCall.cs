using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
   public class OutgoingCall : BaseId
    {
        [Required] public int PhoneUserId { get; set; }
        public PhoneUser Name { get; set; }

        [Required]
        public string OutgoingNumber { get; set; }

        public DateTime TimeOfTheCall { get; set; } = DateTime.Now;
    }
}
