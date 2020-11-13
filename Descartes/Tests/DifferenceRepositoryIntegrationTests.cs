using Descartes.Controllers;
using Descartes.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.Tests
{
    [TestFixture]
    public class DifferenceRepositoryIntegrationTests
    {
        private IDifferenceRepository differenceRepository;
        private DifferenceController differenceController;
        [SetUp]
        public void SetUp()
        {
            differenceRepository = new Mock<IDifferenceRepository>().Object;
            differenceController = new DifferenceController(differenceRepository);
        }
    }
}
