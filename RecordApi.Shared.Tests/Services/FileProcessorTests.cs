using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecordApi.Shared.Model;
using RecordApi.Shared.Services;

namespace RecordApi.Shared.Tests.Services
{
    [TestClass()]
    public class FileProcessorTests
    {
        private const string CSVFile = "csv-records.txt";
        private const string PipeFile = "pipe-records.txt";
        private const string SpaceFile = "space-records.txt";
        private const string Path = "./staticdata";

        [TestMethod()]
        public void GetDelimiterTest()
        {
            var testResult = FileProcessor.GetDelimiter(CSVFile);
            Assert.AreEqual(",", testResult);
            Assert.IsInstanceOfType(testResult, typeof(string));

            testResult = FileProcessor.GetDelimiter(PipeFile);
            Assert.AreEqual("|", testResult);
            Assert.IsInstanceOfType(testResult, typeof(string));

            testResult = FileProcessor.GetDelimiter(SpaceFile);
            Assert.AreEqual(" ", testResult);
            Assert.IsInstanceOfType(testResult, typeof(string));

            testResult = FileProcessor.GetDelimiter("something else");
            Assert.AreEqual(string.Empty, testResult);
            Assert.IsInstanceOfType(testResult, typeof(string));

        }

        [TestMethod()]
        public void LoadRecordsFromDirectoryTest()
        {
            //system under test
            var sut = new FileProcessor();
            
            //run test method
            var testResults = sut.LoadRecordsFromDirectory(Path);
        
            //Assert
            Assert.IsInstanceOfType(testResults,typeof(IEnumerable<IRecord>));
            Assert.AreEqual(17, testResults.Count());

            //Test one row from each file for valid
            //PIPE
            var testRecord = testResults.FirstOrDefault(x => x.LastName == "Brett");

            Assert.AreEqual("Brett", testRecord.LastName);
            Assert.AreEqual("George", testRecord.FirstName);
            Assert.AreEqual("hof3000@gmail.com", testRecord.Email);
            Assert.AreEqual("Blue", testRecord.FavoriteColor);
            Assert.AreEqual(DateTimeOffset.Parse("01/15/1952"), testRecord.DateOfBirth);

            //CSV
            testRecord = testResults.FirstOrDefault(x => x.LastName == "Woodson");

            Assert.AreEqual("Woodson", testRecord.LastName);
            Assert.AreEqual("Rod", testRecord.FirstName);
            Assert.AreEqual("lightning@gmail.com", testRecord.Email);
            Assert.AreEqual("Yellow", testRecord.FavoriteColor);
            Assert.AreEqual(DateTimeOffset.Parse("03/12/1989"), testRecord.DateOfBirth);

            //SPACE
            testRecord = testResults.FirstOrDefault(x => x.LastName == "Jabbar");

            Assert.AreEqual("Jabbar", testRecord.LastName);
            Assert.AreEqual("Kareem", testRecord.FirstName);
            Assert.AreEqual("cap@gmail.com", testRecord.Email);
            Assert.AreEqual("Orange", testRecord.FavoriteColor);
            Assert.AreEqual(DateTimeOffset.Parse("03/01/1978"), testRecord.DateOfBirth);


            var filtredResults = testResults.OrderBy(r => r.LastName);




        }
    }
}