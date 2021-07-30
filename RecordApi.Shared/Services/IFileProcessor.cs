using RecordApi.Shared.Model;
using System.Collections.Generic;

namespace RecordApi.Shared.Services
{
    public interface IFileProcessor
    {

        IEnumerable<IRecord> Records { get; }

        IEnumerable<IRecord> LoadRecordsFromDirectory(string directory);


       
        IRecord AddRecord(Record record, char delimiter = ',');
    }
}
