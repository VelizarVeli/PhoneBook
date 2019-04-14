using System.Collections.Generic;

namespace PhoneBook.App.Core.Contracts
{
    public interface ICommandInterpreter
    {
        string ProcessInput(IList<string> inputParameters);
    }
}