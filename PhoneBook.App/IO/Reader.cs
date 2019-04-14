using System;

namespace PhoneBook.App.IO
{
    public class Reader : IReader

    {
        public string readData()
        {
            return Console.ReadLine();
        }
    }
}
