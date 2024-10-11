namespace GildedRoseKata.ItemStrategies;

public class BackstagePassesItemStrategy : IItemStrategy
{
    public (int sellIn, int quality) UpdateItem(Item item)
    {
        var updatedSellIn = item.SellIn - 1;
        var updatedQuality = 0;

        if (item.SellIn <= 0)
        {
            return (updatedSellIn, updatedQuality);
        }

        if (item.SellIn > 10)
        {
            updatedQuality = item.Quality + 1;
            return (updatedSellIn, updatedQuality);
        }
            
        if (item.SellIn <= 5)
        {
            updatedQuality = item.Quality + 3;
            return (updatedSellIn, updatedQuality);
        }

        if (item.SellIn <= 10)
        {
            updatedQuality = item.Quality + 2;
            return (updatedSellIn, updatedQuality);
        }

        return (updatedSellIn, updatedQuality);
    }
}