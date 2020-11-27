using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.DifferenceDeterminator
{
    public class DifferenceDeterminatorsResolver : IDifferenceDeterminatorResolver
    {
        public string DetermineDifferences(string leftValues, string rightValues)
        {
            DifferenceResponse response = new DifferenceResponse();

            if(leftValues.Length != rightValues.Length)
            {
                response.DiffResultType = "SizeDoNotMatch";
            }
            else if (leftValues == rightValues)
            {
                response.DiffResultType = "Equals";
            }
            else
            {
                response.DiffResultType = "ContentDoNotMatch";
                response.Diffs = GetDifferences(leftValues, rightValues);
            }

            return JsonConvert.SerializeObject(response);
        }

        private List<Difference> GetDifferences(string leftValues, string rightValues)
        {
            List<Difference> differencesResponse = new List<Difference>();

            // index where comparison of left and right part of equation starts to differ
            int indexOfStart = 0;

            // length of that particular consecutive difference
            int length = 0;

            // determine if current comparation needs to have new block or it is a part of already existing difference
            bool different = false;

            // to determine when to insert difference on differencesResponse
            bool shallInsert = false;

            for (int i = 0; i < leftValues.Length; i++)
            {
                // if left and right values are not the same, set different to true, add length, determine start of difference
                if (leftValues[i] != rightValues[i])
                {
                    if (!different)
                    {
                        indexOfStart = i;
                    }

                    different = true;
                    length++;
                    shallInsert = true;
                }
                else
                {
                    // if left and right values are the same
                    // shallInsert is true untill we get matching left and right part
                    if (shallInsert)
                    {
                        differencesResponse.Add(new Difference()
                        {
                            Offset = indexOfStart,
                            Length = length,
                            SubstringDiffereceInLeftPart = leftValues.Substring(indexOfStart, length),
                            SubstringDiffereceInRightPart = rightValues.Substring(indexOfStart, length),
                        });
                        shallInsert = false;
                    }

                    // after insertion reset length, set different to false
                    length = 0;
                    different = false;
                }
            }

            // after we check everything there might be case, when difference is at the end
            // if that is the case we must insert that difference because algorithm does not check if difference stil occurs at the end of the string
            if (length > 0)
            {
                differencesResponse.Add(new Difference()
                {
                    Offset = indexOfStart,
                    Length = length,
                    SubstringDiffereceInLeftPart = leftValues.Substring(indexOfStart, length),
                    SubstringDiffereceInRightPart = rightValues.Substring(indexOfStart, length),
                });
            }

            // return all differences
            return differencesResponse;
        }
    }

    public class DifferenceResponse
    {
        public string DiffResultType { get; set; }
        public List<Difference>? Diffs { get; set; }
    }

    public class Difference
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public string SubstringDiffereceInLeftPart { get; set; }
        public string SubstringDiffereceInRightPart { get; set; }
    }
}
