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
            var regression_pairs = regression_point.regression_pairs;
            for (int i = 0; i < 3; i++)
            {
                regression_pairs = FilterPair.DeleteAbnormalPairs(regression_pairs);
            }
            DrawCorrelationField(regression_pairs);
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
    }

}
