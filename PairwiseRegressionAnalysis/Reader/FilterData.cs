using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairwiseRegressionAnalysis.Reader
{
    internal class FilterData
    {
        public Dictionary<string, List<object>> EmptyDatatoReplace(Dictionary<string, List<object>> titleColumnValues) {
            foreach (var item in titleColumnValues) 
            {
                titleColumnValues[item.Key].ForEach();
            }

            return titleColumnValues;
        }
    }
}
