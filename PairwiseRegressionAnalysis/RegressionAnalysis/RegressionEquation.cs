using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PairwiseRegressionAnalysis.RegressionAnalysis
{
    public static class RegressionEquation
    {
        
        public static (double, double) LinearRegressionCoefficients(List<Point> regression_pairs) 
        {
            double pair_amount = regression_pairs.Count;
            double sumXY = regression_pairs.Sum(pair => pair.X*pair.Y);
            double sumX = regression_pairs.Sum(pair => pair.X);
            double sumY = regression_pairs.Sum(pair => pair.Y);
            double sumXX = regression_pairs.Sum(pair => Math.Pow(pair.X,2));
            double b = (pair_amount * sumXY - sumX * sumY) / (pair_amount * sumXX - Math.Pow(sumX, 2));
            double a = (sumY - b * sumX) / pair_amount;
            return (a, b);
        }
        /*
        public (double, double) ParabolicRegressionCoefficients(List<Point> regression_pairs) 
        {

        }

        public (double, double) HyperbolicRegressionCoefficients(List<Point> regression_pairs) 
        {

        }
        
        public (double, double) LogarithmicRegressionCoefficients(List<Point> regression_pairs) 
        {

        }
        */
    }
}
