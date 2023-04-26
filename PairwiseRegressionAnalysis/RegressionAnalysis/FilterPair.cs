using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MathNet.Numerics.Statistics;

namespace PairwiseRegressionAnalysis.RegressionAnalysis
{
    public static class FilterPair
    {
        private static List<Point> SelectAbnormalPairs(List<Point> regression_pairs, Func<Point, double> getPairValue) 
        {
            double standard_deviation = Statistics.PopulationStandardDeviation(regression_pairs.Select(getPairValue));
            double mean = Statistics.Mean(regression_pairs.Select(getPairValue));
            return regression_pairs.FindAll(pair => Math.Abs(getPairValue(pair) - mean) >= 3 * standard_deviation);
        }

        public static List<Point> DeleteAbnormalPairs(List<Point> regression_pairs) 
        {
            var anomal_regression_pairs = SelectAbnormalPairs(regression_pairs, _pair => _pair.X).ToHashSet()
                .Union(SelectAbnormalPairs(regression_pairs, _pair => _pair.Y));
            regression_pairs.RemoveAll(pair => anomal_regression_pairs.Contains(pair));

            return regression_pairs;
        }
    }
}
