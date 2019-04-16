using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace GildedRose
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class GildedRoseTest
    {
        [Test]
        public void ConjuredItemQuality_DecreasesCorrectly()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Conjured",
                    Quality = 42,
                    SellIn = 30
                }
            };

            var app = new GildedRose(items);
            app.UpdateQuality();
            
            Approvals.Verify(items[0]);
        }
    }
}