using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using RecordParse.API;
using RecordParse.API.Controllers;

namespace RegexParse.Tests.API
{
    [TestFixture]
    public class UT_RecordsController
    {
        private IPersonService _personService;

        [SetUp]
        public void SetUp()
        {
            _personService = Substitute.For<IPersonService>();
        }
        [Test]
        public void RecordsController_PostError_ReturnsBadResult()
        {
            //arrange
            var input = "";
            var message = "Test";
            _personService.Save(input).Throws(new Exception(message));
            var controller = GetController();

            //act
            var result = controller.Post(input) as BadRequestErrorMessageResult;

            //Assert
            Assert.AreEqual(message, result.Message);
        }

        public RecordsController GetController()
        {
            return new RecordsController(_personService);
        }
    }
}
