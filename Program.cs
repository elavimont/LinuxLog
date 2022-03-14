using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LinuxLog
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string val;
                Console.Write("$ mytools ");
                val = Console.ReadLine();

                args = val.Split(' ');
                string location = args[0];

                var dict = val.Split(' ').ToDictionary(x => x.Split('=').First());
                Hashtable newHash = new Hashtable(dict);

                ParsingArgument(newHash, location);
            }
            catch (Exception e)
            {
                //Log(e.ToString());
                Console.WriteLine(e.ToString());
            }
        }
        static StringBuilder GenerateDefaultUsageSyntaxMessage()
        {
            StringBuilder usageSyntax = new StringBuilder();
            usageSyntax.Append("Syntax: " + System.Reflection.Assembly.GetEntryAssembly().GetName().Name + " /$ my tools=n \n");
            usageSyntax.Append("   where:  n = Program Enumeration\n");
            usageSyntax.Append("           1: Log file text type\n");
            usageSyntax.Append("           2: Log file json type\n");
            usageSyntax.Append("           3: Petunjuk penggunaan\n");

            return usageSyntax;
        }

        static int ParsingArgument(Hashtable dict, string location)
        {
            int successIndicator = -1;
            StringBuilder message = new StringBuilder();

            successIndicator = CheckParameter(dict);
            if (successIndicator > 0)
            {
                successIndicator = ProgramSplitter(successIndicator, location);
                if (successIndicator > 0)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Program Run Successfuly.");
                    System.Console.ResetColor();
                    successIndicator = 0; // exit code for successful action
                }
            }


            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Console Finished Running.");
            System.Console.ResetColor();

            return successIndicator;
        }

        

        static int CheckParameter(Hashtable args)
        {
            int programNumber = 0;

            StringBuilder errorMessage = new StringBuilder();
            errorMessage.Append("There was an error while parsing the arguments, please check usage syntax below.");

            if (args.ContainsKey("-t"))
            {
                if (args.ContainsKey("json"))
                {
                    programNumber = 1;
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine(GenerateDefaultUsageSyntaxMessage());
                    System.Console.ResetColor();
                    return programNumber;
                }
                if (args.ContainsKey("text"))
                {
                    programNumber = 2;

                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine(GenerateDefaultUsageSyntaxMessage());
                    System.Console.ResetColor();
                    return programNumber;
                }


            }
            if (args.ContainsKey("-h"))
            {

                programNumber = 3;

                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine(GenerateDefaultUsageSyntaxMessage());
                System.Console.ResetColor();
                return programNumber;

            }
            if (args.ContainsKey("-o"))
            {
                programNumber = 4;
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(errorMessage);
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine(GenerateDefaultUsageSyntaxMessage());
                System.Console.ResetColor();
                return programNumber;
            }
            else
            {
                return 0;
            }


            return programNumber;
        }

        static int ProgramSplitter(int programNumber, string location)
        {
            int result = -1;
            string content = string.Empty;
            string responseContent = string.Empty;
            switch (programNumber)
            {
                case 1:
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Initializing Log File to Text Type");
                    string[] lines = System.IO.File.ReadAllLines(location);

                    foreach (string line in lines)
                    {
                        Console.WriteLine("\t" + line);
                    }

                    Console.WriteLine("Press any key to exit.");
                    System.Console.ReadKey();
                    break;
                case 2:
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    System.Console.WriteLine("Initializing Log File to Json Type");
                    string[] linesJson = System.IO.File.ReadAllLines(location);
                    foreach (string line in linesJson)
                    {
                        JObject parsed = JObject.Parse(line);

                        Console.WriteLine("\t" + line);
                    }

                    Console.WriteLine("Press any key to exit.");
                    System.Console.ReadKey();
                    break;
                case 3:
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    System.Console.WriteLine("Petunjuk Penggunaan");

                    Console.WriteLine("1. Jalankan program" +
                        "\n2. Input perintah contoh C:'\'Users'\'gabriella'\'Documents'\'LogFile.txt -t text"
                        +"\n-perintah -t json untuk file json"
                        + "\n-perintah -t text untuk file text"
                        + "\n-perintah -h untuk petunjuk penggunaan"
                        + "\n3. Tekan enter");
                    break;

                default:
                    {
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Program Not Found!");
                        System.Console.ResetColor();
                        result = 0;
                    }
                    break;
            }

            return result;
        }


    }
}
