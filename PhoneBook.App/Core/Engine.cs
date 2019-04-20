using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.App.Core.Contracts;
using PhoneBook.App.IO;

namespace PhoneBook.App.Core
{
    public class Engine : IEngine

    {
        private readonly IReader reader;
        private IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(IReader reader, IWriter writer, ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            string inputLine;
            while ((inputLine = this.reader.ReadData()) != "End")
            {
                IList<string> inputParameters = inputLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                try
                {
                    string outputResult = this.commandInterpreter.ProcessInput(inputParameters);
                    this.writer.WriteLine(outputResult);
                }
                catch (ArgumentException e)
                {
                    this.writer.WriteLine("ArgumentException: " + e.Message);
                }
                catch (InvalidOperationException e)
                {
                    this.writer.WriteLine("InvalidOperationException: " + e.Message);
                }
                catch (NullReferenceException)
                {
                    this.writer.WriteLine("Invalid command!");
                }
            }
        }
    }
}