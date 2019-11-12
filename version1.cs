using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Command line processor

namespace CLP2
{
    class Program
    {
        // a dictionary is used to map words (commands) on to actions
        static Dictionary<string, Action> commands = new Dictionary<string, Action>();

        static void Main(string[] args)
        {
            // ensure the dictionary has at least  one entry
            commands["help"] = () => Console.WriteLine("I would be delighted to help. Type 'help' for help.");

            // the main repl 
            while (true) { 
              string s = GetInputFromConsole();
              string w = ExtractCommandFromString(s);
              ActionCommand(w);
            }
        }
        
        // The individual steps

        private static void ActionCommand(string w)
        {
            if (commands.ContainsKey(w))
                commands[w]();
            else Console.WriteLine($"I don't know how to {w}");
        }

        private static string ExtractCommandFromString(string s) => s.Split()[0];
        

        static string GetInputFromConsole() => Console.ReadLine().Trim().ToLower();
    }
}
