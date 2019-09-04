using System;
using System.Collections.Generic;
using System.IO;

namespace Kartoteka
{
    public static class Commands
    {
        public static void CreateCommand()
        {
            Record recordToAdd = new Record();

            Console.Write("Enter info:\n" +
                "First name: ");
            recordToAdd.FirstName = Console.ReadLine();

            Console.Write("Last name: ");
            recordToAdd.LastName = Console.ReadLine();

            Console.Write("Date of birth: ");
            while (true)
            {
                try
                {
                    recordToAdd.DateOfBirth = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Wrong time format!");
                    continue;
                }
            }
            try
            {
                DatabaseCommands.InsertRecord(recordToAdd);
                Console.WriteLine("Successfully added record");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void ListCommand(List<string> parameters) => DatabaseCommands.PrintData(parameters);

        public static void StatCommand() => Console.WriteLine(
            DatabaseCommands.AmountOfRecords + " records in database");

        public static void FindCommand(List<string> parameters)
        {
            parameters.RemoveAt(0);
            Dictionary<RecordParameter, object> searchParams = new Dictionary<RecordParameter, object>();

            foreach (var parameter in parameters)
            {
                string parameterStr = parameter.Split(':')[0];
                string valueStr = parameter.Split(':')[1];

                RecordParameter enumParam = StrParamToEnum(parameterStr);
                object parsedValue = null;
                if (enumParam == RecordParameter.WrongParameter)
                {
                    Console.WriteLine("Wrong parameter in find command");
                    return;
                }
                else if (enumParam == RecordParameter.DateOfBirth)
                {
                    DateTime date;
                    if (!DateTime.TryParse(valueStr, out date))
                    {
                        Console.WriteLine("Wrong date of birth in find command");
                        return;
                    }
                    parsedValue = date;
                }
                else if (enumParam == RecordParameter.ID)
                {
                    int idToFind = 0;
                    if (!int.TryParse(valueStr, out idToFind))
                    {
                        Console.WriteLine("Wrong id in find command");
                        return;
                    }
                    parsedValue = idToFind;
                    return;
                }
                else
                {
                    parsedValue = valueStr;
                }
                searchParams.Add(enumParam, parsedValue);
            }
            DatabaseCommands.FindRecords(searchParams);
            
        }

        public static void EditCommand(int id)
        {
            Record recordToEdit = DatabaseCommands.GetRecordById(id);
            if(recordToEdit == null)
            {
                Console.WriteLine("Record not found");
                return;
            }
            Console.WriteLine("Editing record with First name: " + recordToEdit.FirstName);
            Console.Write("First name: ");
            recordToEdit.FirstName = Console.ReadLine();
            Console.Write("Last name: ");
            recordToEdit.LastName = Console.ReadLine();
            Console.Write("Date of birth: ");
            while(true)
            {
                try
                {
                    recordToEdit.DateOfBirth = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Wrong time format!");
                    continue;
                }
            }
            DatabaseCommands.SaveChanges();
            Console.WriteLine("Changes saved");
        }

        public static void PrintMenu()
        {
            Console.WriteLine("Available commands: \n" +
                    "create\n" +
                    "list / list [parameter] ...\n" +
                    "stat\n" +
                    "find [parameter]:[value] [parameter]:[value]...\n" +
                    "edit [id]\n" +
                    "exportCSV\n" +
                    "importCSV\n" +
                    "exportXML\n" +
                    "importXML\n" +
                    "menu\n" +
                    "deleteAll\n" +
                    "remove [id]\n" +
                    "clr");
        }

        public static void ExportXMLCommand(string fileName = "defaultXML.xml")
        {
            try
            {
                DatabaseCommands.ExportXml(fileName);
                Console.WriteLine("Export completed");

            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to XML file \n" + e.Message);

            }
        }
        public static void ImportXMLCommand(string fileName = "defaultXML.xml")
        {
            try
            {
                DatabaseCommands.ImportXml(fileName);
                Console.WriteLine("Loaded successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error importing data from xml file \n" + e.Message);
            }
        }

        public static void ImportCSVCommand(string fileName = "defaultCSV.csv")
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Could not find file in local directory");
                return;
            }
            try
            {
                DatabaseCommands.ImportCsv(fileName);
                Console.WriteLine("Loaded successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not import records from CSV file \n" + e.Message);
            }
        }
        public static void ExportCSVCommand(string fileName = "defaultCSV.csv")
        {
            try
            {
                DatabaseCommands.ExportCsv(fileName);
                Console.WriteLine("Export completed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to CSV file \n" + e.Message);
            }
        }

        public static void DeleteAllRecords()
        {
            if (DatabaseCommands.AmountOfRecords == 0)
            {
                Console.WriteLine("Database is empty, nothing to delete");
                return;
            }
            try
            {
                DatabaseCommands.DeleteAllRecords();
                Console.WriteLine("Deleted every record");
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not delete records \n" + e.Message);
            }
        }
        public static void RemoveRecordById(string idStr)
        {
            if (!int.TryParse(idStr, out int id))
            {
                Console.WriteLine("Wrong parameter for the remove command");
                return;
            }
            DatabaseCommands.RemoveRecord(id);
        }

        public static RecordParameter StrParamToEnum(string parameter)
        {
            if (parameter == "firstname") return RecordParameter.FirstName;
            if (parameter == "lastname") return RecordParameter.LastName;
            if (parameter == "dateofbirth") return RecordParameter.DateOfBirth;
            if (parameter == "id") return RecordParameter.ID;
            return RecordParameter.WrongParameter;
        }

        public enum RecordParameter
        {
            FirstName, LastName, DateOfBirth, ID, WrongParameter
        }
    }
}
