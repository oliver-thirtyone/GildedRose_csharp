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

                HandleQualityOfRegularItem(item);

                DecreaseSellIn(item);

                HandleQualityOfOverdueItem(item);
            }
        }

        private static void HandleQualityOfRegularItem(Item item)
        {
            if (CanItemQualityDecrease(item))
            {
                DecreaseQuality(item);
            }
            else
            {
                IncreaseQuality(item);
            }
        }

        private static void HandleQualityOfOverdueItem(Item item)
        {
            if (item.SellIn < SELL_IN_ZERO)
            {
                if (item.Name == AGED_BRIE)
                {
                    IncreaseQuality(item);
                }
                else if (item.Name == BACKSTAGE_PASSES)
                {
                    item.Quality = MIN_QUALITY;
                }
                else
                {
                    DecreaseQuality(item);
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
            IncreaseQualityIfLowerThanMax(item);

            if (item.Name == BACKSTAGE_PASSES)
            {
                HandleBackstagePassesQuality(item);
            }
        }

        private static void HandleBackstagePassesQuality(Item item)
        {
            if (item.SellIn < BACKSTAGE_QUALITY_TIER_1)
            {
                IncreaseQualityIfLowerThanMax(item);
            }

            if (item.SellIn < BACKSTAGE_QUALITY_TIER_2)
            {
                IncreaseQualityIfLowerThanMax(item);
            }
        }

        private static void IncreaseQualityIfLowerThanMax(Item item)
        {
            if (item.Quality < MAX_QUALITY)
            {
                item.Quality++;
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