using System.Collections.Generic;
using System.Linq;

namespace PairwiseRegressionAnalysis.Reader
{
    internal static class FilterData
    {
        public static Dictionary<string, List<string>> EmptyDatatoNullable(Dictionary<string, List<string>> titleColumnValues) {
            var result = new Dictionary<string, IEnumerable<string>>();
            foreach (var item in titleColumnValues) 
            {
                result[item.Key] = titleColumnValues[item.Key].Select(cell => cell == "-" ? null : cell);
            }
            return result.ToDictionary(column => column.Key, column => column.Value.ToList());
        }


    }
}
