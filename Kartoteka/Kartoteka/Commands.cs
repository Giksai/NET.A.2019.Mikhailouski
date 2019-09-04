using System;
using System.Collections.Generic;
using System.IO;

namespace Kartoteka
{
    /// <summary>
    /// Class that contains all commands for the console
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Command for creating and adding record to the database
        /// </summary>
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
        /// <summary>
        /// Prints all records in database if no additional parameters specified
        /// or prints only specified property of all records in database
        /// </summary>
        /// <param name="parameters">List of properties to display</param>
        public static void ListCommand(List<string> parameters) => DatabaseCommands.PrintData(parameters);

        /// <summary>
        /// Prints current amount of records in database
        /// </summary>
        public static void StatCommand() => Console.WriteLine(
            DatabaseCommands.AmountOfRecords + " records in database");

        /// <summary>
        /// Prints records that has specified values
        /// </summary>
        /// <param name="parameters">Key-value array with key for property and value for record value</param>
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

        /// <summary>
        /// Replaces old record with given id with created record
        /// </summary>
        /// <param name="id">Id of the record to replace</param>
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

        /// <summary>
        /// Prints all available commands
        /// </summary>
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

        /// <summary>
        /// Creates xml file with given name, or with default name,
        /// and fills it with records
        /// </summary>
        /// <param name="fileName">Name of the xml file</param>
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

        /// <summary>
        /// Reads from xml file with given name, or with default name,
        /// and replaces all records with records from the file
        /// </summary>
        /// <param name="fileName">Name of the xml file</param>
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

        /// <summary>
        /// Reads from cvs file with given name, or with default name,
        /// and replaces all records with records from the file
        /// </summary>
        /// <param name="fileName">Name of the cvs file</param>
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

        /// <summary>
        /// Creates cvs file with given name, or with default name,
        /// and fills it with records
        /// </summary>
        /// <param name="fileName">Name of the cvs file</param>
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

        /// <summary>
        /// Permanently deletes all records from the database
        /// </summary>
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

        /// <summary>
        /// Parses id from string to int and
        /// deletes record with given id from database
        /// </summary>
        /// <param name="idStr">id of the record</param>
        public static void RemoveRecordById(string idStr)
        {
            if (!int.TryParse(idStr, out int id))
            {
                Console.WriteLine("Wrong parameter for the remove command");
                return;
            }
            DatabaseCommands.RemoveRecord(id);
        }

        /// <summary>
        /// Converts string parameter to the corresponding enum value
        /// </summary>
        /// <param name="parameter">String representation of the record property</param>
        /// <returns></returns>
        public static RecordParameter StrParamToEnum(string parameter)
        {
            if (parameter == "firstname") return RecordParameter.FirstName;
            if (parameter == "lastname") return RecordParameter.LastName;
            if (parameter == "dateofbirth") return RecordParameter.DateOfBirth;
            if (parameter == "id") return RecordParameter.ID;
            return RecordParameter.WrongParameter;
        }

        /// <summary>
        /// Properties of the record
        /// </summary>
        public enum RecordParameter
        {
            FirstName, LastName, DateOfBirth, ID, WrongParameter
        }
    }
}
