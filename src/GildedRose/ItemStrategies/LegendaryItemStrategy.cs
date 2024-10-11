using System;

namespace GildedRoseKata.ItemStrategies
{
    public class LegendaryItemStrategy : IItemStrategy
    {
        public (int sellIn, int quality) UpdateItem(Item item)
        {
            return (item.SellIn, item.Quality);
        }

        public int ValidateQuality(int quality)
        {
            return quality != 80 ? throw new ArgumentOutOfRangeException(nameof(quality), "Legendary items should always have a quality of 80") : quality;
        }
    }
}