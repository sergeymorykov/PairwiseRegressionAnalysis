using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PairwiseRegressionAnalysis.RegressionAnalysis
{
    public class RegressionPairs : AbstractRegressionPairs
    {
        public readonly new List<Point> regression_pairs = new List<Point>();

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

        private List<Point> SelectAbnormalPairs(Func<Point, double> getPairValue)
        {
            double standard_deviation = Statistics.PopulationStandardDeviation(regression_pairs.Select(getPairValue));
            double mean = Statistics.Mean(regression_pairs.Select(getPairValue));
            return regression_pairs.FindAll(pair => Math.Abs(getPairValue(pair) - mean) >= 3 * standard_deviation);
        }
        public override List<Point> DeleteAbnormalPairs()
        {
            var anomal_regression_pairs = SelectAbnormalPairs(pair => pair.X).ToHashSet()
                .Union(SelectAbnormalPairs(pair => pair.Y));
            regression_pairs.RemoveAll(pair => anomal_regression_pairs.Contains(pair));

            return regression_pairs;
        }        
    }
}
