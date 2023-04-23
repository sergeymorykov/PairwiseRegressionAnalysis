
namespace PairwiseRegressionAnalysis
{
    public class Item<T>
    {
        public readonly T id;
        public readonly string name_item;

        public Item(T id, string name_item)
        {
            this.id = id;
            this.name_item = name_item;
        }

    }
}
