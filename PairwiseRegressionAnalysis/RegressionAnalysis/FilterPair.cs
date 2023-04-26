using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using MathNet.Numerics.Statistics;

namespace PairwiseRegressionAnalysis.RegressionAnalysis
{
    public static class FilterPair
    {
        /*
                   value.RemoveAll(element => {
                       double standard_deviation = Statistics.PopulationStandardDeviation(value);
                       double mean = Statistics.Mean(value);
                       return element < mean - 3 * standard_deviation || element > mean + 3 * standard_deviation;
                   });
        */

        public static List<Point> DeleteAnomalPairs(List<Point> regression_pairs) 
        {
            double standard_deviation_X = Statistics.PopulationStandardDeviation(regression_pairs.Select(pair => pair.X));
            double mean_X = Statistics.Mean(regression_pairs.Select(pair => pair.X));
            double standard_deviation_Y = Statistics.PopulationStandardDeviation(regression_pairs.Select(pair => pair.Y));
            double mean_Y = Statistics.Mean(regression_pairs.Select(pair => pair.Y));

            regression_pairs.RemoveAll(pair => 
                pair.X <= mean_X - 3 * standard_deviation_X || 
                pair.X >= mean_X + 3 * standard_deviation_X ||
                pair.Y <= mean_Y - 3 * standard_deviation_Y || 
                pair.Y >= mean_Y + 3 * standard_deviation_Y);

            return regression_pairs;
        }
    }
}
