using System.Collections.Generic;

namespace PhoneBook.App.Core.Contracts
{
    public interface IManager
    {
        void Load();

        string Add(IList<string> arguments);

        string Delete(IList<string> arguments);

        string ShowPhoneNumber(IList<string> arguments);

        string PrintAll(IList<string> arguments);

        string OutgoingCall(IList<string> arguments);

        string ShowOutgoingCalls(IList<string> arguments);
    }
}
