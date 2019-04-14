using System.Collections.Generic;

namespace PhoneBook.App.Models
{
    public class User
    {
        public User()
        {
            this.OutgoingCalls = new List<string>();
        }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<string> OutgoingCalls { get; set; }
    }
}
