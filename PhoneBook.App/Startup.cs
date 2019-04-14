using PhoneBook.App.Core;
using PhoneBook.App.Core.Contracts;
using PhoneBook.App.IO;

namespace PhoneBook.App
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new Reader();
            IWriter writer = new Writer();
            IManager manager = new PhoneBookManager();
            ICommandInterpreter command = new CommandInterpreter(manager);
            var engine = new Engine(reader, writer, command);
            engine.Run();
        }
    }
}
