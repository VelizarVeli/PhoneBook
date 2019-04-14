using System;
using System.Text;

namespace PhoneBook.App.IO
{
    public class Writer : IWriter
    {
        private readonly StringBuilder sb;

        public Writer()
        {
            this.sb = new StringBuilder();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}