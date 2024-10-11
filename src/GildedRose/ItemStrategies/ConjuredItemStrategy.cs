namespace GildedRoseKata.ItemStrategies
{
    public class ConjuredItemStrategy : IItemStrategy
    {
        public (int sellIn, int quality) UpdateItem(Item item)
        {
            var updatedQuality = item.Quality - 2;
            var updatedSellIn = item.SellIn - 1;

            if (item.SellIn <= 0)
            {
                updatedQuality -= 2;
            }

            return (updatedSellIn, updatedQuality);
        }
    }
}