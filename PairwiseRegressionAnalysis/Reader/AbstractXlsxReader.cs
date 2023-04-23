using Aspose.Cells;
using System.Collections.Generic;

namespace PairwiseRegressionAnalysis.Reader
{
    public abstract class AbstractXlsxReader
    {
        public readonly Cells cells;

        public AbstractXlsxReader(Cells cells)
        {
            this.cells = cells;
        }

        public abstract List<object> GetColumn(int column_index);

        public abstract List<object> GetRow(int row_index);

        public abstract List<string> TitlesColumn(out int last_row_index);

        public abstract Dictionary<string, List<object>> GetColumnValues();
    }
}
