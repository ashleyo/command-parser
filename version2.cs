// Intended to be a little more sophisticated and practical. This version introduces the following changes over version 1.
// 1) 'commands' are members of an enum, not just strings
// 2) This enables features such as easily listing all the commands
// 3) A static constructor is added for the Main Class to allow easier initialization of the command dictionary

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Command line processor version 2

namespace CLP2
{
    class Program
    {
        // command dictionary is changed to use the enum type for its keys
        static Dictionary<CommandWords, Action> commands = new Dictionary<CommandWords, Action>();

        // valid commands are ennumerated
        public enum CommandWords { none, help, tango }
        
        // used to keep a list of commands as a string (utility for the help command)
        public static string CommandList { get; }

        static Program() // a use case for a static constructor - initializing static data
        {
            // populate the dictionary
            commands[CommandWords.none] = () => Console.WriteLine($"I don't know how to do that!");
            commands[CommandWords.tango] = () => Console.WriteLine("With me! 1-2-1-2-1-2-3");
            commands[CommandWords.help] = () => Console.WriteLine($"I would be delighted to help! The commands I recognize are \n{CommandList} ");

            // populate the list of  commands for help
            StringBuilder sb = new StringBuilder();
            Enum.GetNames(typeof(CommandWords)).Where(s => s != "none").ToList().ForEach(s => sb.AppendLine(s));
            CommandList = sb.ToString();
        }

        // in the below, 'string' is changed to 'CommandWords' as needed
        static void Main(string[] args)
        {         
            while (true)
            {
                string s = GetInputFromConsole();
                CommandWords w = ExtractCommandFromString(s);
                ActionCommand(w);
            }
        }

        private static void ActionCommand(CommandWords w) => commands[w]();

        // parse the input string to a CommandWords type, returning CommandWords.none if unrecognized.
        private static CommandWords ExtractCommandFromString(string s)      
            => Enum.TryParse<CommandWords>(s.Split()[0], out CommandWords result) ? result : CommandWords.none;
    
        static string GetInputFromConsole() => Console.ReadLine().Trim().ToLower();
    }
}

