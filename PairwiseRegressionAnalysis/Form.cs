using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using Aspose.Cells;
using PairwiseRegressionAnalysis.Reader;
using PairwiseRegressionAnalysis.RegressionAnalysis;
using MathNet.Numerics;
using System.Security;
using static System.Net.Mime.MediaTypeNames;

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
            for (int i = 0; i < 2; i++)
            {
                regression_pairs = regression_point.DeleteAbnormalPairs();
            }
            DrawCorrelationField(regression_pairs);

            var correlation_coefficient = StatisticsFunction.GetCorrelationCoefficient(regression_pairs);
            labelCorrelationCoefficient.Text = Math.Round(correlation_coefficient, 6).ToString();
            var label_correlation_test_text = labelCorrelationTest.Text;
            label_correlation_test_text = label_correlation_test_text.Remove(label_correlation_test_text.IndexOf('-') + 2);
            label_correlation_test_text += СoefficientSignificanceTest(regression_pairs) ? "значим" : "не значим";
            labelCorrelationTest.Text = label_correlation_test_text;

            (double a, double b) = RegressionEquation.LinearRegressionCoefficients(regression_pairs);
            labelRegrassionEquation.Text = $"y = {Math.Round(a,3)} + {Math.Round(b,3)}x";

            DrawRegressionEquation(regression_pairs, x => a + b * x, "линейная регрессия");

            labelRegressionError.Text = Math.Round(StatisticsFunction.GetRegressionError(regression_pairs, x => a + b * x), 6).ToString();

        }

        public static bool СoefficientSignificanceTest(List<Point> regression_pairs)
        {
            var analytical_student_criterion_value = StatisticsFunction.GetAnalyticalStudentCriterion(regression_pairs);
            int amount_pair = regression_pairs.Count;
            var table_student_criterion_value = StatisticsFunction.GetTableStudentCriterion(amount_pair, 0.95);
            return Math.Abs(analytical_student_criterion_value) > table_student_criterion_value;
        }

        private void DrawCorrelationField(List<Point> points)
        {
            chart_regression.Series["SeriesCorrelationField"].Points.Clear();
            chart_regression.Series["SeriesCorrelationField"].LegendText = "Поле корреляции";
            foreach (var point in points)
            {
                chart_regression.Series["SeriesCorrelationField"].Points.AddXY(point.X, point.Y);
            }
        }

        private void DrawRegressionEquation(List<Point> points, Func<double, double> regression_func, string regression_func_name)
        {
            chart_regression.Series["SeriesRegressionEquation"].Points.Clear();
            chart_regression.Series["SeriesRegressionEquation"].LegendText = regression_func_name;
            foreach (var point in points)
            {
                chart_regression.Series["SeriesRegressionEquation"].Points.AddXY(point.X, regression_func(point.X));
            }
        }
    }
}
