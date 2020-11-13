using Descartes.DifferenceDeterminator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.Tests
{
    [TestFixture]
    public class DifferenceDeterminatorsResolverTest: DifferenceDeterminatorsResolver
    {
        public DifferenceDeterminatorsResolverTest() { }

        [Test]
        public void ShouldReturnValidDiffs()
        {/*
            // Arrange
            DifferenceResponse diffResult1 = new DifferenceResponse()
            {
                DiffResultType = "ContentDoNotMatch",
                Diffs = new List<Difference>()
                {
                    new Difference()
                    {
                         Offset = 0,
                         Length = 1,
                         SubstringDiffereceInLeftPart = "A",
                         SubstringDiffereceInRightPart = "B"
                    },
                    new Difference()
                    {
                         Offset = 2,
                         Length = 2,
                         SubstringDiffereceInLeftPart = "AA",
                         SubstringDiffereceInRightPart = "BB"
                    },
                    new Difference()
                    {
                         Offset = 4,
                         Length = 3,
                         SubstringDiffereceInLeftPart = "AAA",
                         SubstringDiffereceInRightPart = "BBB"
                    }
                }
            };

            DifferenceResponse diffResult2 = new DifferenceResponse()
            {
                DiffResultType = "SizeDoNotMatch"
            };

            DifferenceResponse diffResult3 = new DifferenceResponse()
            {
                DiffResultType = "Equals"
            };

            // Act
            DifferenceResponse result1 = determineDifferences("AAAAAAAA", "BABBABBB");
            DifferenceResponse result2 = determineDifferences("AAA", "BABBABBB");
            DifferenceResponse result3 = determineDifferences("AAA", "AAA");

            // Assert
            Assert.AreEqual(result1, diffResult1);
            Assert.AreEqual(result2, diffResult2);
            Assert.AreEqual(result3, diffResult3);*/
        }
    }
}
