using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordApi.Shared.Services
{
    public class RecordOutPutService : IRecordOutPutService
    {
        private readonly IFileProcessor _fileProcessor;

        public RecordOutPutService()
        {
        }

        public RecordOutPutService(IFileProcessor fileProcessor)
        {
            _fileProcessor = fileProcessor;
        }


        public void RecordsSortedByColor()
        {

            var records = _fileProcessor.Records.OrderBy(r => r.FavoriteColor).ThenBy(r => r.LastName);

            Console.WriteLine($"Listing Records in Order by Favorite Color and then Last Name");

            foreach (var record in records)
            {
                Console.WriteLine($"Color: {record.FavoriteColor}; LastName: {record.LastName}; FirstName: {record.FirstName}; Email: {record.Email}; DOB: {record.DateOfBirth:d}");
            }
        }

        public void RecordsSortedByDateOfBirth()
        {

            var records = _fileProcessor.Records.OrderBy(r => r.DateOfBirth);

            Console.WriteLine($"Listing Records in Order by Date of Birth");

            foreach (var record in records)
            {
                Console.WriteLine($"DOB: {record.DateOfBirth:d}; LastName: {record.LastName}; FirstName: {record.FirstName}; Email: {record.Email}; Color: {record.FavoriteColor}");
            }
        }

        public void RecordsSortedByLastNameDescending()
        {

            var records = _fileProcessor.Records.OrderByDescending(r => r.LastName);

            Console.WriteLine($"Listing Records in Order by Last Name DESC");

            foreach (var record in records)
            {
                Console.WriteLine($"LastName: {record.LastName}; FirstName: {record.FirstName}; Email: {record.Email}; Color: {record.FavoriteColor}; DOB: {record.DateOfBirth:d}");
            }
        }
    }
}
