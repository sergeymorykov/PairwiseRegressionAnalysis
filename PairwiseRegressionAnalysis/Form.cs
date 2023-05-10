using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using Aspose.Cells;
using PairwiseRegressionAnalysis.Reader;
using PairwiseRegressionAnalysis.RegressionAnalysis;

namespace PairwiseRegressionAnalysis
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private Dictionary<string, List<string>> district_data = new Dictionary<string, List<string>>();

        private void Form1_Load(object sender, EventArgs e)
        {
            Workbook wb = new Workbook("xlsx/2021.xlsx");
            WorksheetCollection collection = wb.Worksheets;
            Worksheet worksheet = collection[0];
            XlsxReader xlsxReader = new XlsxReader(worksheet.Cells);
            district_data = FilterData.EmptyDatatoNullable(xlsxReader.GetColumnValues());
            district_data.Remove(district_data.First().Key); //delete column with district name
            comboBoxX.Items.AddRange(district_data.Keys.ToArray());
            comboBoxY.Items.AddRange(district_data.Keys.ToArray());
        }

        private Item<int> selectItem_comboBoxX;
        private Item<int> selectItem_comboBoxY;
        private void comboBoxX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectItem_comboBoxX != null) comboBoxY.Items.Insert(selectItem_comboBoxX.id, selectItem_comboBoxX.name_item);
            comboBoxY.Items.Remove(comboBoxX.SelectedItem);
            selectItem_comboBoxX = new Item<int>(comboBoxX.SelectedIndex, comboBoxX.SelectedItem.ToString());
        }
        private void comboBoxY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectItem_comboBoxY != null) comboBoxX.Items.Insert(selectItem_comboBoxY.id, selectItem_comboBoxY.name_item);
            comboBoxX.Items.Remove(comboBoxY.SelectedItem);
            selectItem_comboBoxY = new Item<int>(comboBoxY.SelectedIndex, comboBoxY.SelectedItem.ToString());
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            var first_elements_pair = district_data[comboBoxX.SelectedItem.ToString()];
            var second_elements_pair = district_data[comboBoxY.SelectedItem.ToString()];
            var regression_point = new RegressionPairs(first_elements_pair, second_elements_pair);

            var regression_pairs = regression_point.DeleteAbnormalPairs();
            for (int i = 0; i < 1; i++) { 
                regression_pairs = regression_point.DeleteAbnormalPairs();
            }
            DrawPoints(regression_pairs, "Поле корреляции","SeriesCorrelationField");

            (double a, double b) = RegressionEquation.LinearRegressionCoefficients(regression_pairs);
            labelRegrassionEquation.Text = $"y = {Math.Round(a,3)} + {Math.Round(b,3)}x";

            DrawRegressionEquation(regression_pairs, x => a + b * x, "линейная регрессия", "SeriesRegressionEquation");        

            double t_critical = StatisticsFunction.GetTableStudentCriterion(regression_pairs.Count, 0.95);
            double standartDeviationA = StatisticsFunction.GetStandartDeviationA(regression_pairs, x => a + b * x);
            double standartDeviationB = StatisticsFunction.GetStandartDeviationB(regression_pairs, x => a + b * x);
            
            double predicted_x = 1.1 * regression_pairs.Max(x => x.X);
            double minX = regression_pairs.Min(x => x.X);
            List<Point> list_min_to_max = CreateListMinToMax(minX, predicted_x, 40);

            double bottom_line_a = a - t_critical * standartDeviationA;
            double bottom_line_b = b - t_critical * standartDeviationB;
            DrawRegressionEquation(list_min_to_max, x => bottom_line_a + bottom_line_b * x, "нижняя граница", "SeriesBottomLine");

            double upper_bound_a = a + t_critical * standartDeviationA;
            double upper_bound_b = b + t_critical * standartDeviationB;
            DrawRegressionEquation(list_min_to_max, x => upper_bound_a + upper_bound_b * x, "верхняя граница", "SeriesUpperBound");

            Point predicted_point = new Point(predicted_x, a + b * predicted_x);
            DrawPoints(new List<Point> {predicted_point}, "точечный прогноз", "SeriesPredictedPoint");

            double sigma_auf = GetSigmaAuf(regression_pairs, predicted_point, x => a + b * x);

            Point upper_bound_predicted_point = new Point(predicted_x, predicted_point.Y + t_critical * sigma_auf);       
            DrawPoints(new List<Point> { upper_bound_predicted_point }, "Верхняя граница прогноза", "SeriesUpperBoundPredicted");

            Point bottom_line_predicted_point = new Point(predicted_x, predicted_point.Y - t_critical * sigma_auf);
            DrawPoints(new List<Point> { bottom_line_predicted_point }, "Нижняя граница прогноза", "SeriesBottomLinePredicted");

            labelApproximationError.Text = StatisticsFunction.GetApproximationError(regression_pairs, x => a + b * x).ToString();
        }

        public static double GetSigmaAuf(List<Point> regression_pairs, Point predicted_point, Func<double, double> regression_func) 
        {
            double avg_x = regression_pairs.Average(x => x.X);
            double regression_error = StatisticsFunction.GetRegressionError(regression_pairs, regression_func);
            double difference_avg_x_predicted_x = predicted_point.X - avg_x;
            double difference_sum_x_avg_x = regression_pairs.Sum(x => Math.Pow(x.X-avg_x,2));
            return regression_error * Math.Sqrt(1 + 1 / regression_pairs.Count + Math.Pow(difference_avg_x_predicted_x, 2) / difference_sum_x_avg_x);
        }

        public static List<Point> CreateListMinToMax(double min, double max, double count) 
        {
            List<Point> result = new List<Point>();
            for (double i = min; i <= max; i += (max-min)/count) 
            {
                result.Add(new Point(i, 0));
            }
            return result;
        }

        public static bool СoefficientSignificanceTest(List<Point> regression_pairs)
        {
            var analytical_student_criterion_value = StatisticsFunction.GetAnalyticalStudentCriterion(regression_pairs);
            int amount_pair = regression_pairs.Count;
            var table_student_criterion_value = StatisticsFunction.GetTableStudentCriterion(amount_pair, 0.95);
            return Math.Abs(analytical_student_criterion_value) > table_student_criterion_value;
        }

        private void DrawPoints(List<Point> points, string regression_func_name, string label_name)
        {
            chart_regression.Series[label_name].Points.Clear();
            chart_regression.Series[label_name].LegendText = regression_func_name;
            chart_regression.ChartAreas[0].AxisX.Title = "X";
            chart_regression.ChartAreas[0].AxisY.Title = "Y";
            foreach (var point in points)
            {
                chart_regression.Series[label_name].Points.AddXY(point.X, point.Y);
            }
        }

        private void DrawRegressionEquation(List<Point> points, Func<double, double> regression_func, string regression_func_name, string label_name)
        {
            chart_regression.Series[label_name].Points.Clear();
            chart_regression.Series[label_name].LegendText = regression_func_name;
            chart_regression.ChartAreas[0].AxisX.Title = "X";
            chart_regression.ChartAreas[0].AxisY.Title = "Y";
            foreach (var point in points)
            {
                chart_regression.Series[label_name].Points.AddXY(point.X, regression_func(point.X));
            }
        }
    }
}
