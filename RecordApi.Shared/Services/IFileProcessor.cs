using RecordApi.Shared.Model;
using System.Collections.Generic;

namespace RecordApi.Shared.Services
{
    public interface IFileProcessor
    {

        IEnumerable<Record> Records { get; }

        IEnumerable<Record> LoadRecordsFromDirectory(string directory);


       
        Record AddRecord(Record record, char delimiter = '*');
    }
}
