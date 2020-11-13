using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.DifferenceDeterminator
{
    public class DifferenceDeterminatorsResolver : IDifferenceDeterminatorResolver
    {
        public string determineDifferences(string leftValues, string rightValues)
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
                response.Diffs = getDifferences(leftValues, rightValues);
            }

            return JsonConvert.SerializeObject(response);
        }

        private List<Difference> getDifferences(string leftValues, string rightValues)
        {
            List<Difference> differencesResponse = new List<Difference>();
            int indexOfStart = 0;
            int length = 0;
            bool different = false;
            bool shallInsert = false;

            for (int i = 0; i < leftValues.Length; i++)
            {
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

                    length = 0;
                    different = false;
                }
            }

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
