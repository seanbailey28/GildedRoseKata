namespace GildedRoseKata.ItemStrategies
{
    public class StandardItemStrategy : IItemStrategy
    {
        public (int sellIn, int quality) UpdateItem(Item item)
        {
            var updatedSellIn = item.SellIn - 1;
            var updatedQuality = item.Quality - 1;
            if (item.SellIn <= 0)
            {
                updatedQuality--;
            }

            return (updatedSellIn, updatedQuality);
        }
    }
}
