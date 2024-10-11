using GildedRoseKata.ItemStrategies;
using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                var context = new ItemStrategyContext(item.Name);
                var (sellIn, quality) = context.UpdateItem(item);
                item.SellIn = sellIn;
                item.Quality = quality;
            }
        }
    }
}
