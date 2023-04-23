using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aspose.Cells;
using MathNet.Numerics.Statistics;

namespace PairwiseRegressionAnalysis
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private static List<object> GetColumn(Cells elements, int column_index)
        {
            List<object> elements_column = new List<object>();

            int row_amount = elements.MaxDataRow;
            for (int row_index = 0; row_index <= row_amount; row_index++) elements_column.Add(elements[row_index, column_index].Value);

            return elements_column;
        }
        private static List<string> TitlesReader(Worksheet worksheet, out int last_row_index)
        {
            List<string> title = new List<string>();

            last_row_index = 0;
            int column_amount = worksheet.Cells.MaxDataColumn;
            for (int column_index = 0; column_index <= column_amount; column_index++)
            {
                int row_index = GetColumn(worksheet.Cells, column_index).FindIndex((element) => element != null);

                if (last_row_index < row_index) last_row_index = row_index;
            }
            for (int column_index = 0; column_index <= column_amount; column_index++)
            {
                int row_index = GetColumn(worksheet.Cells, column_index).GetRange(0, last_row_index + 1).FindLastIndex((element) => element != null);
                title.Add(worksheet.Cells[row_index, column_index].Value.ToString());
            }

            return title;
        }
        private static Dictionary<string, List<object>> XlsxReader(Worksheet worksheet) //GetColumnValues
        {
            Dictionary<string, List<object>> result = new Dictionary<string, List<object>>();
            List<string> title_name = new List<string>();
            title_name = TitlesReader(worksheet, out int last_titles_row_index);

            int row_amount = worksheet.Cells.MaxDataRow;
            int column_amount = worksheet.Cells.MaxDataColumn;
            for (int column_index = 0; column_index <= column_amount; column_index++)
            {
                int column_range_count = row_amount - last_titles_row_index - 1;
                var values = GetColumn(worksheet.Cells, column_index).GetRange(last_titles_row_index + 1, column_range_count);
                //values.RemoveAll(element => element == null);
                result.Add(title_name[column_index], values);
            }

            return result;
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

            var data = XlsxReader(worksheet);
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
