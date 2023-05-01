using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PairwiseRegressionAnalysis.RegressionAnalysis
{
    public abstract class AbstractRegressionPairs
    {
        public readonly List<Point> regression_pairs = new List<Point>();
        public abstract List<Point> DeleteAbnormalPairs();
    }
}
