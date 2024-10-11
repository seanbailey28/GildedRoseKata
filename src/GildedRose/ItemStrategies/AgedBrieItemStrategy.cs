namespace GildedRoseKata.ItemStrategies;

public class AgedBrieItemStrategy : IItemStrategy
{
    public (int sellIn, int quality) UpdateItem(Item item)
    {
        var updatedQuality = item.Quality + 1;
        var updatedSellIn = item.SellIn - 1;

        // Not included in the requrements but Aged Brie quality increases by 2 after sellIn date
        if (item.SellIn <= 0)
        {
            updatedQuality++;
        }

        return (updatedSellIn, updatedQuality);
    }
}