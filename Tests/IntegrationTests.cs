using Descartes.Controllers;
using Descartes.DifferenceDeterminator;
using Descartes.Entities;
using Descartes.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class IntegrationTests
    {
        public IntegrationTests() { }

        [TestMethod]
        public void OnValidInputDatabaseWasCalled()
        {
            var responseString = @"{""DiffResultType"":""Equals"",""Diffs"":null}";
            var mockedRepository = new Mock<IDifferenceRepository>();
            mockedRepository.Setup(repository => repository.DetermineDifferences(It.IsAny<int>())).Returns(responseString);
            DifferenceController controller = new DifferenceController(mockedRepository.Object);

            var response = controller.GetDifferences(12);

            mockedRepository.Verify(repository => repository.DetermineDifferences(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void OnNullInputDatabaseShouldNotBeCalled()
        {
            var responseString = @"{""DiffResultType"":""Equals"",""Diffs"":null}";
            var mockedRepository = new Mock<IDifferenceRepository>();
            mockedRepository.Setup(repository => repository.DetermineDifferences(It.IsAny<int>())).Returns(responseString);
            DifferenceController controller = new DifferenceController(mockedRepository.Object);

            var response = controller.SaveRightPartOfEquation(new RequestDifferenceInputHelper()
            {
                data = null
            }, 12);

            mockedRepository.Verify(repository => repository.DetermineDifferences(It.IsAny<int>()), Times.Never);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void OnValidInputDatabaseShouldNotBeCalledWithSave()
        {
            var responseString = @"{""DiffResultType"":""Equals"",""Diffs"":null}";
            var mockedRepository = new Mock<IDifferenceRepository>();
            mockedRepository.Setup(repository => repository.DetermineDifferences(It.IsAny<int>())).Returns(responseString);
            DifferenceController controller = new DifferenceController(mockedRepository.Object);
            var requestInput = new RequestDifferenceInputHelper()
            {
                data = "testString"
            };

            var response = controller.SaveRightPartOfEquation(requestInput, 12);

            mockedRepository.Verify(repository => repository.SaveObject("right", requestInput, 12), Times.Once);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetAllDataFromDataBaseShouldBeCalled()
        {
            var mockedRepository = new Mock<IDifferenceRepository>();
            mockedRepository.Setup(repository => repository.GetAllDatabaseContent()).Returns(new List<DifferenceObject>());
            DifferenceController controller = new DifferenceController(mockedRepository.Object);

            var response = controller.GetAllDatabaseContent();

            mockedRepository.Verify(repository => repository.GetAllDatabaseContent(), Times.Once);
            Assert.IsNotNull(response);
        }
    }
}
