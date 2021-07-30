using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecordApi.Shared.Services;

namespace RecordApi.Shared.Tests.Services
{
    [TestClass()]
    public class RecordOutPutServiceTests
    {
        [TestMethod()]
        public void RecordsSortedByColorTest()
        {
            //Test Output Will List Records as expected.
            var sut = new RecordOutPutService(new FileProcessor());

            sut.RecordsSortedByColor();
            sut.RecordsSortedByDateOfBirth();
            sut.RecordsSortedByLastNameDescending();

        }
    }
}