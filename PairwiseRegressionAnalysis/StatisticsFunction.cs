using Aspose.Cells;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using PairwiseRegressionAnalysis.Reader;
using PairwiseRegressionAnalysis.RegressionAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace PairwiseRegressionAnalysis
{
    public static class StatisticsFunction
    {
        public static double GetCorrelationCoefficient(List<Point> pairs) =>
            Correlation.Pearson(pairs.Select(pair => pair.X), pairs.Select(pair => pair.Y));

        public static double GetAnalyticalStudentCriterion(List<Point> pairs) 
        {
            var correlation_coefficient = GetCorrelationCoefficient(pairs);
            var amount_pair = pairs.Count;
            return correlation_coefficient * Math.Sqrt((amount_pair-2)/(1-Math.Pow(correlation_coefficient,2)));
        }

        public static double GetRegressionError(List<Point> pairs, Func<double, double> regression_func) 
        {
            double difference_sum = pairs.Sum(pair => Math.Pow(pair.Y - regression_func(pair.X), 2));
            return Math.Sqrt(difference_sum / (pairs.Count - 2));
        }

        public static double GetDispersionX(List<Point> pairs)
        {
            double sum = pairs.Sum(pair => pair.X);
            double sum_of_squares = pairs.Sum(pair => pair.X*pair.X);
            return sum_of_squares - Math.Pow(sum, 2);
        }

        public static double GetStandartDeviationA(List<Point> pairs, Func<double, double> regression_func) 
        {
            double regression_error = GetRegressionError(pairs, regression_func);
            double dispersionX = GetDispersionX(pairs);
            double sum_of_squares = pairs.Sum(pair => pair.X * pair.X);
            double pair_amount = pairs.Count;
            return regression_error * Math.Sqrt(sum_of_squares) / (pair_amount * dispersionX);
        }
        public static double GetStandartDeviationB(List<Point> pairs, Func<double, double> regression_func)
        {
            double regression_error = GetRegressionError(pairs, regression_func);
            double dispersionX = GetDispersionX(pairs);
            double pair_amount = pairs.Count;
            return regression_error / (Math.Sqrt(pair_amount) * dispersionX);
        }

        public static double GetAnalyticalStudentCriterionA(List<Point> pairs, Func<double, double> regression_func, double a)
        {
            return 1.0 * a / GetStandartDeviationA(pairs, regression_func);
        }

        public static double GetAnalyticalStudentCriterionB(List<Point> pairs, Func<double, double> regression_func, double b)
        {
            return 1.0 * b / GetStandartDeviationB(pairs, regression_func);
        }
        public static double GetTableStudentCriterion(int degrees_of_freedom, double significance_lavel) 
        {
            /* нуждается в доработке
            Workbook wb = new Workbook("xlsx/StudentCriterion.xlsx");
            WorksheetCollection collection = wb.Worksheets;
            Worksheet worksheet = collection[0];
            XlsxReader xlsxReader = new XlsxReader(worksheet.Cells);
            int significance_lavel_index = xlsxReader.GetTitlesColumn().FindIndex(significance_lavel_string => 
                significance_lavel_string == significance_lavel.ToString());
            return Convert.ToDouble(worksheet.Cells[degrees_of_freedom, significance_lavel_index].Value);
            */
            return 1.99;
        }
    }
}
