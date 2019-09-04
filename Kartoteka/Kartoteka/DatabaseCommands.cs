using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Kartoteka
{
    public static class DatabaseCommands
    {
        private static ApplicationContext context = new ApplicationContext();

        public static int AmountOfRecords
        {
            get
            {
                return context.Records.Count();
            }
        }

        public static void InsertRecord(string firstName, string lastName, DateTime dateOfBirth)
        {
                context.Records.Add(new Record
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth
                });
                context.SaveChanges();
        }
        public static void RemoveRecord(int id)
        {
            if(context[id] == null)
            {
                Console.WriteLine("Database does not have record with given id");
                return;
            }
            context.Records.Remove(context[id]);
            context.SaveChanges();
            Console.WriteLine("Successfully removed record");
        }
        public static void InsertRecord(Record record)
        {
                context.Records.Add(record);
                context.SaveChanges();
        }

        public static void PrintData(List<string> parameters)
        {
            if (AmountOfRecords == 0)
            {
                Console.WriteLine("Database is empty");
                return;
            }

            parameters.RemoveAt(0);

            List<Commands.RecordParameter> recParameters = new List<Commands.RecordParameter>();

            foreach(var parameter in parameters)
                recParameters.Add(Commands.StrParamToEnum(parameter));

            if(recParameters.Contains(Commands.RecordParameter.WrongParameter))
            {
                Console.WriteLine("Wrong parameter in list command");
                return;
            }

            var records = context.Records;
            foreach (var record in records)
            {
                var data = new StringBuilder();

                if(recParameters.Count > 0)
                {
                    if (recParameters.Contains(Commands.RecordParameter.ID))
                        data.AppendLine($"ID: {record.RecordId}");
                    if (recParameters.Contains(Commands.RecordParameter.FirstName))
                        data.AppendLine($"First Name: {record.FirstName}");
                    if (recParameters.Contains(Commands.RecordParameter.LastName))
                        data.AppendLine($"Last Name: {record.LastName}");
                    if (recParameters.Contains(Commands.RecordParameter.DateOfBirth))
                        data.AppendLine($"Date of birth: {record.DateOfBirth}");
                }
                else
                {
                     data.AppendLine($"ID: {record.RecordId}");
                     data.AppendLine($"First Name: {record.FirstName}");
                     data.AppendLine($"Last Name: {record.LastName}");
                     data.AppendLine($"Date of birth: {record.DateOfBirth}");
                }
                Console.WriteLine("Found records: ");
                    Console.WriteLine(data.ToString());
            }
        }

        public static void FindRecords(Dictionary<Commands.RecordParameter, object> searchParams)
        {
            IEnumerable<Record> searchRecords = context.Records.ToList();
            for (int i = 0; i < searchParams.Keys.Count; i++)
            {
                Commands.RecordParameter key = searchParams.Keys.ElementAt(i);
                if (key == Commands.RecordParameter.FirstName)
                    searchRecords = from e in searchRecords
                                    where e.FirstName == (string)searchParams[key]
                              select e;
                if (key == Commands.RecordParameter.LastName)
                    searchRecords = from e in searchRecords
                              where e.LastName == (string)searchParams[key]
                              select e;
                if (key == Commands.RecordParameter.DateOfBirth)
                    searchRecords = from e in searchRecords
                              where e.DateOfBirth == (DateTime)searchParams[key]
                              select e;
                if (key == Commands.RecordParameter.ID)
                    searchRecords = from e in searchRecords
                              where e.RecordId == (int)searchParams[key]
                              select e;
            }
            
            if (searchRecords.Count() == 0)
            {
                Console.WriteLine("No records found");
                return;
            }
            Console.WriteLine($"Found {searchRecords.Count()} records:");
            PrintData(searchRecords);
        }

        public static void PrintData(IEnumerable<Record> records)
        {
            foreach (var record in records)
            {
                var data = new StringBuilder();
                data.AppendLine($"ID: {record.RecordId}");
                data.AppendLine($"First Name: {record.FirstName}");
                data.AppendLine($"Last Name: {record.LastName}");
                data.AppendLine($"Date of birth: {record.DateOfBirth}");
                Console.WriteLine(data.ToString());
            }
        }

        public static Record GetRecordById(int id)
        {
            foreach(var record in context.Records)
                if(record.RecordId == id)
                    return record;

            return null;
        }

        public static bool HasRecord(int id)
        {
            foreach (var record in context.Records)
                if (record.RecordId == id)
                    return true;

            return false;
        }

        public static void SaveChanges() => context.SaveChanges();

        public static void ExportCsv(string fileName)
        {
                using (var csv = new CsvWriter(new StreamWriter(fileName)))
                {
                    csv.WriteRecords(context.Records);
                }
        }

        public static void ExportXml(string fileName)
        {
                XElement recordElement = new XElement("Records",
                    (from rec in context.Records
                    select new
                    {
                        rec.RecordId,
                        rec.FirstName,
                        rec.LastName,
                        rec.DateOfBirth
                    }).ToList().Select(
                        x => new XElement("Record",
                        new XAttribute("RecordId", x.RecordId),
                        new XAttribute("FirstName", x.FirstName),
                        new XAttribute("LastName", x.LastName),
                        new XAttribute("DateOfBirth", x.DateOfBirth)
                        )));

                recordElement.Save(fileName);
        }
        public static void ImportXml(string fileName)
        {
            try
            {
                if(AmountOfRecords != 0)
                DeleteAllRecords();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error at clearing database before importing from xml file\n" +
                    e.Message);
                return;
            }
            using (FileStream str = new FileStream(fileName, FileMode.Open))
            {
                XElement recordsXml = XElement.Load(str);
                foreach(var recordXml in recordsXml.Elements())
                {
                    Record recordToAdd = new Record();
                    recordToAdd.RecordId = int.Parse(recordXml.Attribute("RecordId").Value);
                    recordToAdd.FirstName = recordXml.Attribute("FirstName").Value;
                    recordToAdd.LastName = recordXml.Attribute("LastName").Value;
                    recordToAdd.DateOfBirth = DateTime.Parse(recordXml.Attribute("DateOfBirth").Value);
                    context.Records.Add(recordToAdd);
                }
                context.SaveChanges();
            }
        }


        public static void ImportCsv(string fileName)
        {
            using(var csv = new CsvReader(File.OpenText(fileName)))
            {
                IEnumerable<Record> records = csv.GetRecords<Record>();

                try
                {
                    if (AmountOfRecords != 0)
                        DeleteAllRecords();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error at clearing database before importing from csv file\n" +
                    e.Message);
                    return;
                }
                foreach(var record in records)
                    context.Records.Add(record);

                context.SaveChanges();
            }
        }
        
        public static void DeleteAllRecords()
        {
            foreach (var record in context.Records)
                context.Records.Remove(record);

            context.SaveChanges();
        }
    }
}
