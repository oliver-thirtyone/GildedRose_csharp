using System.Collections.Generic;

namespace GildedRose
{
    public class GildedRose
    {
        private const string AGED_BRIE = "Aged Brie";
        private const string SULFURAS_HAND_OF_RAGNAROS = "Sulfuras, Hand of Ragnaros";
        private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
        private const string CONJURED = "Conjured";
        private const int MAX_QUALITY = 50;
        private const int BACKSTAGE_QUALITY_TIER_1 = 11;
        private const int BACKSTAGE_QUALITY_TIER_2 = 6;
        private const int MIN_QUALITY = 0;
        private const int SELL_IN_ZERO = 0;
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name == SULFURAS_HAND_OF_RAGNAROS)
                {
                    continue;
                }

                if (CanItemQualityDecrease(item))
                {
                    DecreaseQuality(item);
                }
                else
                {
                    IncreaseQuality(item);
                }

                DecreaseSellIn(item);

                HandleOverdueItem(item);
            }
        }

        private static void HandleOverdueItem(Item item)
        {
            if (item.SellIn < SELL_IN_ZERO)
            {
                switch (item.Name)
                {
                    case AGED_BRIE:
                        IncreaseQuality(item);
                        break;
                    case BACKSTAGE_PASSES:
                        item.Quality = MIN_QUALITY;
                        break;
                    default:
                        DecreaseQuality(item);
                        break;
                }
            }
        }

        private static bool CanItemQualityDecrease(Item item)
        {
            return item.Name != AGED_BRIE
                   && item.Name != BACKSTAGE_PASSES;
        }

        private static void DecreaseSellIn(Item item)
        {
            item.SellIn--;
        }

        private static void IncreaseQuality(Item item)
        {
            if (item.Quality < MAX_QUALITY)
            {
                item.Quality = item.Quality + 1;

                if (item.Name == BACKSTAGE_PASSES)
                {
                    if (item.SellIn < BACKSTAGE_QUALITY_TIER_1)
                    {
                        if (item.Quality < MAX_QUALITY)
                        {
                            item.Quality++;
                        }
                    }

                    if (item.SellIn < BACKSTAGE_QUALITY_TIER_2)
                    {
                        if (item.Quality < MAX_QUALITY)
                        {
                            item.Quality++;
                        }
                    }
                }
            }
        }

        private static void DecreaseQuality(Item item)
        {
            if (item.Quality > MIN_QUALITY)
            {
                item.Quality--;

                if (item.Name == CONJURED)
                {
                    item.Quality--;
                }
            }
        }
    }
}