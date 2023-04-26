using System;
using System.Collections.Generic;
using System.Windows;

namespace PairwiseRegressionAnalysis.RegressionAnalysis
{
    public class RegressionPairs
    {
        public readonly List<Point> regression_pairs = new List<Point>();

        public RegressionPairs(List<string> first_elements_pair, List<string> second_elements_pair) 
        {
            if (first_elements_pair.Count != second_elements_pair.Count) throw new Exception("the numbet of elements in the list doesn't match");

            int pair_amount = first_elements_pair.Count;
            for (int pair_index = 0; pair_index < pair_amount; pair_index++) 
            {
                if (first_elements_pair[pair_index] != null && second_elements_pair[pair_index] != null)
                    regression_pairs.Add(new Point(Double.Parse(first_elements_pair[pair_index]),
                        Double.Parse(second_elements_pair[pair_index])));                 
            }
        }
    }
}
