using RecordApi.Shared.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecordApi.Shared.Services
{
    public class FileProcessor : IFileProcessor
    {
        private const string DataPath = "./staticdata";
        private const string CsvFileName = "csv-records.txt";
        private const string PipeFileName = "pipe-records.txt";
        private const string SpaceFileName = "space-records.txt";
        public FileProcessor()
        {
            Records = LoadRecordsFromDirectory(DataPath);
        }

        public IEnumerable<IRecord> Records { get; set; }


        public IEnumerable<IRecord> LoadRecordsFromDirectory(string directory)
        {
            var records = new List<IRecord>();

            foreach (var filePath in Directory.GetFiles(directory))
            {
                var lines = File.ReadAllLines(filePath, Encoding.UTF8);

                records.AddRange(lines.Select(line => line.Split(GetDelimiter(filePath)))
                    .Select(values => new Record
                    {
                        LastName = values[0],
                        FirstName = values[1],
                        Email = values[2],
                        FavoriteColor = values[3],
                        DateOfBirth = DateTimeOffset.Parse(values[4])
                    }));
            }
            return records.ToArray();
        }

        public IRecord AddRecord(Record record, char delimiter = ',')
        {
            //going to add record to csv by default but allow an optional delimiter parameter

            
            var line = new[]
            {
                string.Join(delimiter, record.LastName, record.FirstName, record.Email, record.FavoriteColor,
                    record.DateOfBirth.ToString("d"))
            };
            File.AppendAllLines(Path.Combine(DataPath, GetFileName(delimiter)),line, Encoding.UTF8);
            Records = LoadRecordsFromDirectory(DataPath);
            return record;

        }

        public static string GetFileName(char delimiter) =>
            delimiter switch
            {
                ',' => CsvFileName,
                '|' => PipeFileName,
                ' ' => SpaceFileName,
                _ => throw new ArgumentOutOfRangeException(nameof(delimiter), delimiter, null)
            };

        public static string GetDelimiter(string filePath) =>
            filePath switch
            {
                { } fileName when fileName.Contains("csv") => ",",
                { } fileName when fileName.Contains("pipe") => "|",
                { } fileName when fileName.Contains("space") => " ",
                _ => string.Empty
            };
    }
}
