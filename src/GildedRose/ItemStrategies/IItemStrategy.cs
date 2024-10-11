namespace GildedRoseKata.ItemStrategies;

public interface IItemStrategy
{
    (int sellIn, int quality) UpdateItem(Item item);

    int ValidateQuality(int quality)
    {
        if (quality < 0)
        {
            return 0;
        }

        if (quality > 50)
        {
            return 50;
        }

        return quality;
    }
}

