using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PhoneBook.App.Core.Contracts;
using PhoneBook.App.Models;

namespace PhoneBook.App.Core
{
    public class PhoneBookManager : IManager
    {
        private readonly IDictionary<string, User> phonesDictionary;
        private PhoneNumberCheck check;

        public PhoneBookManager()
        {
            this.phonesDictionary = new Dictionary<string, User>();
            this.Load();
        }

        public void Load()
        {
            Console.Write("Write the path to the file and the file name with extension:");
            string path = Console.ReadLine();
            var phonebookFIle = File.ReadAllText(path).Split(new[] { Environment.NewLine },
                StringSplitOptions.None);
            var checkPhoneNormalBg = new PhoneNumberCheck();
            foreach (var phone in phonebookFIle)
            {
                var name = phone.Split()[0];
                var phoneNumber = phone.Split()[1];

                if (checkPhoneNormalBg.Check(phoneNumber))
                {
                    if (!phonesDictionary.ContainsKey(name))
                    {
                        phonesDictionary.Add(name, new User { Name = name, PhoneNumber = phoneNumber });
                    }
                }
            }
        }

        public string Add(IList<string> arguments)
        {
            string name = arguments[1];
            string phoneNumber = arguments[2];
            var checkPhoneNormalBg = new PhoneNumberCheck();

            if (checkPhoneNormalBg.Check(phoneNumber))
            {
                if (!phonesDictionary.ContainsKey(name))
                {
                    phonesDictionary.Add(name, new User() { Name = name, PhoneNumber = phoneNumber });
                }
                else
                {
                    return $"{name} already exists in contacts!";
                }
                return $"Phone number for {name} added successfully.";
            }

            return "Invalid entry!";
        }

        public string Delete(IList<string> arguments)
        {
            var name = arguments[1];
            if (phonesDictionary.ContainsKey(name))
            {
                phonesDictionary.Remove(name);
            }
            else
            {
                return $"{name} doesn't exist in contacts!";
            }
            return $"Phone number for {name} deleted successfully.";
        }

        public string ShowPhoneNumber(IList<string> arguments)
        {
            string name = arguments[1];
            if (phonesDictionary.ContainsKey(name))
            {
                var phoneNumber = phonesDictionary.FirstOrDefault(x => x.Key == name).Value;
                return $"{name}'s phone number is {phoneNumber.PhoneNumber}";
            }
            return $"{name} doesn't exist in contacts!";
        }

        public string PrintAll(IList<string> arguments)
        {
            var sb = new StringBuilder();
            foreach (var name in phonesDictionary.OrderBy(n => n.Key))
            {
                sb.AppendLine($"{name.Key} {name.Value.PhoneNumber}");
            }

            return sb.ToString().Trim();
        }

        public string OutgoingCall(IList<string> arguments)
        {
            string name = arguments[1];
            string callingToNumber = arguments[2];
            if (phonesDictionary.ContainsKey(name))
            {
                phonesDictionary[name].OutgoingCalls.Add(callingToNumber);
                return $"Calling {callingToNumber} ...";
            }

            return $"There is no such name!";
        }

        public string ShowOutgoingCalls(IList<string> arguments)
        {
            var sb = new StringBuilder();

            var counter = 1;

            var orderedByOutgoingCount = phonesDictionary.OrderByDescending(c => c.Value.OutgoingCalls.Count).ThenBy(n => n.Key);
            foreach (var name in orderedByOutgoingCount)
            {
                sb.AppendLine($"{name.Key} with phone number {name.Value.PhoneNumber} has {name.Value.OutgoingCalls.Count} outgoing calls.");
                if (counter == 5)
                {
                    break;
                }

                counter++;
            }
            return sb.ToString().Trim();
        }
    }
}
