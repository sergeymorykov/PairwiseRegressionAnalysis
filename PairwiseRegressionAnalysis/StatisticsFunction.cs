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
    internal static class StatisticsFunction
    {
        public static double GetMeanX(List<Point> pairs) => Statistics.Mean(pairs.Select(pair => pair.X));
        public static double GetMeanY(List<Point> pairs) => Statistics.Mean(pairs.Select(pair => pair.Y));
        public static double GetCorrelationCoefficient(List<Point> pairs) =>
            Correlation.Pearson(pairs.Select(pair => pair.X), pairs.Select(pair => pair.Y));

        public static double GetAnalyticalStudentCriterion(List<Point> pairs) 
        {
            var correlation_coefficient = GetCorrelationCoefficient(pairs);
            var amount_pair = pairs.Count;
            return correlation_coefficient * Math.Sqrt((amount_pair-2)/(1-Math.Pow(correlation_coefficient,2)));
        }

        /* нуждается в доработке
        public static double GetTableStudentCriterion(int degrees_of_freedom, double significance_lavel) 
        {
            Workbook wb = new Workbook("xlsx/StudentCriterion.xlsx");
            WorksheetCollection collection = wb.Worksheets;
            Worksheet worksheet = collection[0];
            XlsxReader xlsxReader = new XlsxReader(worksheet.Cells);
            int significance_lavel_index = xlsxReader.GetTitlesColumn().FindIndex(significance_lavel_string => 
                significance_lavel_string == significance_lavel.ToString());
            return Convert.ToDouble(worksheet.Cells[degrees_of_freedom, significance_lavel_index].Value);
        }
        */
    }
}
