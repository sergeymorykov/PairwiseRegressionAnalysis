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
        public override List<object> GetColumn(int column_index)
        {
            List<object> cells_column = new List<object>();

            int row_amount = cells.MaxDataRow;
            for (int row_index = 0; row_index <= row_amount; row_index++) cells_column.Add(cells[row_index, column_index].Value);

            return cells_column;
        }

        public override List<object> GetRow(int row_index)
        {
            List<object> cells_row = new List<object>();

            int column_amount = cells.MaxDataColumn;
            for (int column_index = 0; column_index <= column_amount; column_index++) cells_row.Add(cells[row_index, row_index].Value);

            return cells_row;
        }

        public override List<string> TitlesColumn(out int last_row_index) 
        {
            List<string> title_column = new List<string>();

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
                title_column.Add(cells[row_index, column_index].Value.ToString());
            }

            return title_column;
        }
    }
}
