using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PhoneBook.App.Core.Contracts;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.App.Core
{
    public class PhoneBookManager : IManager
    {
        private readonly ICollection<PhoneUser> phoneBook;
        private readonly PhoneNumberCheck checkPhoneNormalBg;
        private readonly PhoneBookDbContext dbContext;

        public PhoneBookManager()
        {
            this.phoneBook = new List<PhoneUser>();
            this.dbContext = new PhoneBookDbContext();
            this.checkPhoneNormalBg = new PhoneNumberCheck();
            this.Load();
        }

        public void Load()
        {
            string path = @"../../../file.txt";
            var phonebookFIle = File.ReadAllText(path).Split(new[] { Environment.NewLine },
                StringSplitOptions.None);
            foreach (var phone in phonebookFIle)
            {
                var name = phone.Split()[0];
                var phoneNumber = phone.Split()[1];

                if (checkPhoneNormalBg.Check(phoneNumber))
                {
                    var checkUser = dbContext.Users.Any(a => a.Name == name);
                    if (!checkUser)
                    {
                        var currentUser = new PhoneUser { Name = name, PhoneNumber = phoneNumber };
                        phoneBook.Add(currentUser);
                    }
                }
                dbContext.Users.UpdateRange(phoneBook);
                dbContext.SaveChanges();
            }
        }

        public string Add(IList<string> arguments)
        {
            string name = arguments[1];
            string phoneNumber = arguments[2];

            if (checkPhoneNormalBg.Check(phoneNumber))
            {
                var checkUser = dbContext.Users.Any(a => a.Name == name);
                if (!checkUser)
                {
                    var currentUser = new PhoneUser { Name = name, PhoneNumber = phoneNumber };
                    dbContext.Users.Update(currentUser);
                    dbContext.SaveChanges();
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
            var currentUser = dbContext.Users.FirstOrDefault(a => a.Name == name);
            if (currentUser != null)
            {
                dbContext.Users.Remove(currentUser);
                dbContext.SaveChanges();
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
            var currentUser = dbContext.Users.FirstOrDefault(a => a.Name == name);
            if (currentUser != null)
            {
                return $"{name}'s phone number is {currentUser.PhoneNumber}";
            }
            return $"{name} doesn't exist in contacts!";
        }

        public string PrintAll(IList<string> arguments)
        {
            var sb = new StringBuilder();
            var allUsers = dbContext.Users.OrderBy(n => n.Name).ToList();
            foreach (var user in allUsers)
            {
                sb.AppendLine($"{user.Name} {user.PhoneNumber}");
            }

            return sb.ToString().Trim();
        }

        public string OutgoingCall(IList<string> arguments)
        {
            string name = arguments[1];
            string callingToNumber = arguments[2];

            var currentUser = dbContext.Users.FirstOrDefault(a => a.Name == name);
            if (currentUser != null)
            {
                var outgoingCall = new OutgoingCall
                {
                    PhoneUserId = currentUser.Id,
                    OutgoingNumber = callingToNumber
                };
                currentUser.OutgoingCalls.Add(outgoingCall);
                dbContext.OutgoingCalls.Add(outgoingCall);
                dbContext.SaveChanges();
                return $"Calling {callingToNumber} ...";
            }

            return "There is no such name!";
        }

        public string ShowOutgoingCalls(IList<string> arguments)
        {
            var sb = new StringBuilder();

            var orderedByOutgoingCount = dbContext
                .Users
                .OrderByDescending(c => c.OutgoingCalls.Count)
                .ThenBy(n => n.Name)
                .Take(5);

            foreach (var user in orderedByOutgoingCount)
            {
                sb.AppendLine($"{user.Name} with phone number {user.PhoneNumber} has {user.OutgoingCalls.Count} outgoing calls.");
            }
            return sb.ToString().Trim();
        }
    }
}
