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
    public class RecordOutPutServiceTests
    {
        [TestMethod()]
        public void RecordsSortedByColorTest()
        {
            var sut = new RecordOutPutService(new FileProcessor());

            sut.RecordsSortedByColor();
            sut.RecordsSortedByDateOfBirth();
            sut.RecordsSortedByLastNameDescending();

        }
    }
}