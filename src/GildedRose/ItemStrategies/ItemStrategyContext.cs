namespace GildedRoseKata.ItemStrategies
{
    public class ItemStrategyContext
    {
        private readonly IItemStrategy _strategy = null;

        public ItemStrategyContext(string itemName)
        {
            _strategy = itemName switch
            {
                "Aged Brie" => new AgedBrieItemStrategy(),
                "Backstage passes to a TAFKAL80ETC concert" => new BackstagePassesItemStrategy(),
                "Sulfuras, Hand of Ragnaros" => new LegendaryItemStrategy(),
                "Conjured Mana Cake" => new ConjuredItemStrategy(),
                _ => new StandardItemStrategy(),
            };
        }

        public (int sellIn, int quality) UpdateItem(Item item)
        {
            var (sellIn, quality) = _strategy.UpdateItem(item);
            var validatedQuality = _strategy.ValidateQuality(quality);
            return (sellIn, validatedQuality);
        }
    }
}
