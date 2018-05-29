using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CleaningRobot.Lib;

namespace CleaningRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialize request...");
            Console.WriteLine("Start engine...");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("RUN!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            if (args.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR!!!\nYou should execute this program with passing 2 arguments. For example :");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("cleaning_robot source.json result.json");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("the first argument should be \"source.json\" or \"source1.json\"");
                Console.WriteLine("please try again with that format");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Input file {args[0]} does not exist");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            try
            {
                var robot = new Lib.CleaningRobot();
                var request = new Request();

                using (var source = new StreamReader(args[0]))
                {
                    var input = source.ReadToEnd();
                    request = JsonConvert.DeserializeObject<Request>(input);
                }

                Console.WriteLine(".\n..\n...");
                Console.WriteLine("Robot is running...");
                var result = robot.Run(request);


                Console.WriteLine(".\n..\n...");
                Console.WriteLine("Generating result file");
                using (var dest = new StreamWriter(args[1]))
                {
                    var output = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                    dest.Write(output);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unhandled Exception executing program: {ex.Message}");
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Stop Engine...");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Note: you can found the result file in the same folder/path with this robot.\nThank you!");
            }
        }
    }
}
