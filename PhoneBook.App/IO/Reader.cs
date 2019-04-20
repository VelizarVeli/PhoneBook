using System;

namespace PhoneBook.App.IO
{
    public class Reader : IReader

    {
        public string ReadData()
        {
            return Console.ReadLine();
        }
    }
}
