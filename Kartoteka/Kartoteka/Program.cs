using System;
using System.Collections.Generic;

namespace Kartoteka
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Checking database existence...");
                using (var context = new ApplicationContext())
                    context.Database.EnsureCreated();
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }

            Commands.PrintMenu();
            while (true)
            {
                Console.Write(">");
                List<string> inputCommand = new List<string>(Console.ReadLine().Split(' '));

                switch (inputCommand[0])
                {
                    case "create":
                            Commands.CreateCommand();
                            break;
                    case "list":
                            if(inputCommand.Count > 4)
                            Console.WriteLine("Wrong amount of parameters in the list command");
                            else
                            Commands.ListCommand(inputCommand);
                            break;
                    case "stat":
                            Commands.StatCommand();
                            break;
                    case "find":
                        if(inputCommand.Count == 1 || inputCommand.Count > 4)
                            Console.WriteLine("Wrong parameters fot the find command");
                        else Commands.FindCommand(new List<string>(inputCommand));
                        break;
                    case "edit":
                        if (inputCommand.Count == 2)
                            Commands.EditCommand(int.Parse(inputCommand[1]));
                        else Console.WriteLine("Wrong parameters for the edit command");
                            break;
                    case "menu":
                            Commands.PrintMenu();
                            break;
                    case "importCSV":
                        if(inputCommand.Count == 1)
                            Commands.ImportCSVCommand();
                        else if(inputCommand.Count == 2)
                            Commands.ImportCSVCommand(inputCommand[1]);
                        else
                            Console.WriteLine("Wrong parameters for the importCSV command");
                        break;
                    case "exportCSV":
                        if (inputCommand.Count == 1)
                            Commands.ExportCSVCommand();
                        else if (inputCommand.Count == 2)
                            Commands.ExportCSVCommand(inputCommand[1]);
                        else
                            Console.WriteLine("Wrong parameters for the exportCSV command");
                        break;
                    case "importXML":
                        if (inputCommand.Count == 1)
                            Commands.ImportXMLCommand();
                        else if (inputCommand.Count == 2)
                            Commands.ImportXMLCommand(inputCommand[1]);
                        else
                            Console.WriteLine("Wrong parameters for the importXML command");
                        break;
                    case "exportXML":
                        if (inputCommand.Count == 1)
                            Commands.ExportXMLCommand();
                        else if (inputCommand.Count == 2)
                            Commands.ExportXMLCommand(inputCommand[1]);
                        else
                            Console.WriteLine("Wrong parameters for the exportXML command");
                        break;
                    case "clr":
                        Console.Clear();
                        break;
                    case "deleteAll":
                        Commands.DeleteAllRecords();
                        break;
                    case "remove":
                        if(inputCommand.Count != 2)
                        {
                            Console.WriteLine("Wrong amount of parameters for the remove command");
                            break;
                        }
                        Commands.RemoveRecordById(inputCommand[1]);
                        break;
                    default:
                            Console.WriteLine($"Command '{inputCommand[0]} not recognized'");
                            break;
                }
            }
        }
    }
}
