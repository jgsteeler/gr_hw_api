using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecordApi.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordApi.Shared.Services.Tests
{
    [TestClass()]
    public class FileProcessorTests
    {
        private const string CSVFile = "csv-records.txt";
        private const string PipeFile = "pipe-records.txt";
        private const string SpaceFile = "space-records.txt";

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
    }
}