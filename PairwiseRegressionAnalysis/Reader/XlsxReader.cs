using Aspose.Cells;
using System.Collections.Generic;


namespace PairwiseRegressionAnalysis.Reader
{
    internal class XlsxReader : AbstractXlsxReader
    {
        public new readonly Cells cells;

        public XlsxReader(Cells cells) : base(cells)
        {
            this.cells = cells;
        }

        public override List<string> GetColumn(int column_index)
        {
            var result = new List<string>();

            int row_amount = cells.MaxDataRow;
            for (int row_index = 0; row_index <= row_amount; row_index++) {
                result.Add(cells[row_index, column_index].Value?.ToString());
            }

            return result;
        }

        public override List<string> GetRow(int row_index)
        {
            var result = new List<string>();

            int column_amount = cells.MaxDataColumn;
            for (int column_index = 0; column_index <= column_amount; column_index++) result.Add(cells[row_index, column_index].Value?.ToString());

            return result;
        }

        public override List<string> GetTitlesColumn() 
        {
            var result = new List<string>();

            int last_row_index = 0;
            int column_amount = cells.MaxDataColumn;
            for (int column_index = 0; column_index <= column_amount; column_index++)
            {
                int row_index = GetColumn(column_index).FindIndex((element) => element != null);
                if (last_row_index < row_index) last_row_index = row_index;
            }
            for (int column_index = 0; column_index <= column_amount; column_index++)
            {
                int row_index = GetColumn(column_index).GetRange(0, last_row_index + 1).FindLastIndex((element) => element != null);
                result.Add(cells[row_index, column_index].Value.ToString());
            }

            return result;
        }
        public override List<string> GetTitlesColumn(out int last_row_index) 
        {
            var result = new List<string>();

            last_row_index = 0;
            int column_amount = cells.MaxDataColumn;
            for (int column_index = 0; column_index <= column_amount; column_index++)
            {
                int row_index = GetColumn(column_index).FindIndex((element) => element != null);
                if (last_row_index < row_index) last_row_index = row_index;
            }
            for (int column_index = 0; column_index <= column_amount; column_index++)
            {
                int row_index = GetColumn(column_index).GetRange(0, last_row_index + 1).FindLastIndex((element) => element != null);
                result.Add(cells[row_index, column_index].Value.ToString());
            }

            return result;
        }

        public override Dictionary<string, List<string>> GetColumnValues() 
        {
            var result = new Dictionary<string, List<string>>();
            List<string> title_name = GetTitlesColumn(out int last_titles_row_index);

            int row_amount = cells.MaxDataRow;
            int column_amount = cells.MaxDataColumn;
            for (int column_index = 0; column_index <= column_amount; column_index++)
            {
                int column_range_count = row_amount - last_titles_row_index - 1;
                var values = GetColumn(column_index).GetRange(last_titles_row_index + 1, column_range_count);
                result.Add(title_name[column_index], values);
            }

            return result;
        }
    }
}
