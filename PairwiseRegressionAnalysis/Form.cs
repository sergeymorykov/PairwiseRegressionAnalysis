using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aspose.Cells;
using PairwiseRegressionAnalysis.Reader;
using MathNet.Numerics.Statistics;

namespace PairwiseRegressionAnalysis
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void DrawCorrelationField(List<Point> points)
        {
            chart_regression.Series["SeriesCorrelationField"].Points.Clear();
            chart_regression.Series["SeriesCorrelationField"].LegendText = "Поле корреляции";
            foreach (var point in points)
            {
                chart_regression.Series["SeriesCorrelationField"].Points.AddXY(point);
            }
        }


        private static double? ObjectToDouble(object element)
        {
            double value;
            if (element == null) return null;
            else if (Double.TryParse(element.ToString(), out value)) return value;
            throw new Exception("Cannot implicitly convert type 'object' to 'double?'");
        }

        private Dictionary<string, List<double?>> district_data = new Dictionary<string, List<double?>>();

        private void Form1_Load(object sender, EventArgs e)
        {
            Workbook wb = new Workbook("xlsx/2021.xlsx");
            WorksheetCollection collection = wb.Worksheets;
            Worksheet worksheet = collection[0];
            XlsxReader xlsxReader = new XlsxReader(worksheet.Cells);
            var data = FilterData.EmptyDatatoNullable(xlsxReader.GetColumnValues());
            data.Remove(data.First().Key);
            foreach (var keyValue in data)
            {
                try
                {
                    var value = keyValue.Value.ConvertAll(ObjectToDouble);
                    /*
                    value.RemoveAll(element => {
                        double standard_deviation = Statistics.PopulationStandardDeviation(value);
                        double mean = Statistics.Mean(value);
                        return element < mean - 3 * standard_deviation || element > mean + 3 * standard_deviation;
                    });
                    */
                    district_data.Add(keyValue.Key, value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
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

        }
    }

}
