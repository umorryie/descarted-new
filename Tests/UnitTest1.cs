using Descartes.DifferenceDeterminator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DifferenceDeterminatorsResolverTest : DifferenceDeterminatorsResolver
    {
        public DifferenceDeterminatorsResolverTest() { }

        [TestMethod]
        public void ShouldReturnValidDiffs()
        {
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
                         Offset = 5,
                         Length = 3,
                         SubstringDiffereceInLeftPart = "AAA",
                         SubstringDiffereceInRightPart = "BBB"
                    },
                    new Difference()
                    {
                         Offset = 9,
                         Length = 1,
                         SubstringDiffereceInLeftPart = "A",
                         SubstringDiffereceInRightPart = "C"
                    },
                    new Difference()
                    {
                         Offset = 11,
                         Length = 1,
                         SubstringDiffereceInLeftPart = "A",
                         SubstringDiffereceInRightPart = "C"
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
            string result1 = determineDifferences("AAAAAAAAAAAA", "BABBABBBACAC");
            string result2 = determineDifferences("AAA", "BABBABBB");
            string result3 = determineDifferences("AAA", "AAA");

            DifferenceResponse result11 = JsonConvert.DeserializeObject<DifferenceResponse>(result1);
            DifferenceResponse result22 = JsonConvert.DeserializeObject<DifferenceResponse>(result2);
            DifferenceResponse result33 = JsonConvert.DeserializeObject<DifferenceResponse>(result3);

            // Assert
            Assert.IsNotNull(result11);
            Assert.IsNotNull(result22);
            Assert.IsNotNull(result33);

            Assert.IsNotNull(result11.DiffResultType);
            Assert.IsNotNull(result22.DiffResultType);
            Assert.IsNotNull(result33.DiffResultType);

            Assert.IsNotNull(result11.Diffs);
            Assert.IsNull(result22.Diffs);
            Assert.IsNull(result33.Diffs);

            Assert.AreEqual(result22.DiffResultType, diffResult2.DiffResultType);
            Assert.AreEqual(result33.DiffResultType, diffResult3.DiffResultType);

            Assert.AreEqual(result11.DiffResultType.Length, diffResult1.DiffResultType.Length);

            foreach (var (diff, index) in diffResult1.Diffs.Select((value, i) => (value, i)))
            {
                Assert.AreEqual(diff.Length, result11.Diffs[index].Length);
                Assert.AreEqual(diff.SubstringDiffereceInLeftPart, result11.Diffs[index].SubstringDiffereceInLeftPart);
                Assert.AreEqual(diff.SubstringDiffereceInRightPart, result11.Diffs[index].SubstringDiffereceInRightPart);
                Assert.AreEqual(diff.Offset, result11.Diffs[index].Offset);
            }
        }
    }
}
