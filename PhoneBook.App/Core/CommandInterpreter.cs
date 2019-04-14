using System.Collections.Generic;
using System.Linq;
using PhoneBook.App.Core.Contracts;

namespace PhoneBook.App.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IManager phoneBookManager;

        public CommandInterpreter(IManager phoneBookManager)
        {
            this.phoneBookManager = phoneBookManager;
        }

        public string ProcessInput(IList<string> inputParameters)
        {
            string command = inputParameters[0];

            string result = (string)this.phoneBookManager
                .GetType()
                .GetMethods()
                .FirstOrDefault(m => m.Name.Contains(command))
                .Invoke(this.phoneBookManager, new object[] { inputParameters });

            return result;
        }
    }
}
