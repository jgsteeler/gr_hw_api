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
        private const string Path = "./staticdata";

        public FileProcessor()
        {
            Records = LoadRecordsFromDirectory(Path);
        }

        public IEnumerable<IRecord> Records { get; }


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
