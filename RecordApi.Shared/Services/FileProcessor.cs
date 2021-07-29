﻿using RecordApi.Shared.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordApi.Shared.Services
{
    public class FileProcessor 
    {
        private const string Path = "./staticdata";
        private IEnumerable<IRecord> _records;
        public FileProcessor()
        {
            _records = LoadRecordsFromDirectory(Path);
        }

        public IEnumerable<IRecord> Records => _records;
        
        
        public IEnumerable<IRecord> LoadRecordsFromDirectory(string directory)
        {
            var records = new List<IRecord>();

            foreach (var filePath in GetFileNames(directory))
            {
                var lines = File.ReadAllLines(filePath, Encoding.UTF8);

                foreach (var line in lines)
                {
                    string[] values = line.Split(GetDelimiter(filePath));
                    records.Add(new Record
                    {
                        LastName = values[0],
                        FirstName = values[1],
                        Email = values[2],
                        FavoriteColor = values[3],
                        DateOfBirth = DateTimeOffset.Parse(values[4])
                    });
                }
            }
            return records.ToArray();
        }

        public string[] GetFileNames(string path)
        {
            return Directory.GetFiles(path);
        }

        public static string GetDelimiter(string filePath)
        {
            var delimiter = string.Empty;
            switch (filePath)
            {
                case string fileName when fileName.Contains("csv"):
                    delimiter = ",";
                    break;
                case string fileName when fileName.Contains("pipe"):
                    delimiter = "|";
                    break;
                case string fileName when fileName.Contains("space"):
                    delimiter = " ";
                    break;
                default:
                    break;
            }

            return delimiter;
        }

       
    }
}
