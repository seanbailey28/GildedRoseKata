using Xunit;

using AutoFixture;
using System.Linq;
using FluentAssertions;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        private readonly IFixture _fixture;

        public GildedRoseTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Given_ItemNameIsFoo_When_QualityUpdateOccurs_Then_NameStaysTheSame()
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.Name, "foo")
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Name.Should().Be("foo"));
        }

        [Fact]
        public void Given_SellInDaysIsOne_When_QualityUpdateOccurs_Then_DaysDecreasesByOne()
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.SellIn, 1)
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.SellIn.Should().Be(0));
        }

        [Fact]
        public void Given_SellByDateHasNotPassed_When_QualityUpdateOccurs_Then_QualityDegradesByOne()
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.SellIn, 7)
                .With(x => x.Quality, 5)
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(4));
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(-1, 5)]
        [InlineData(-100, 5)]
        public void Given_SellByDateHasPassed_When_QualityUpdateOccurs_Then_QualityDegradesTwiceAsFast(int sellIn, int quality)
        {
            // Arrange
            var items = _fixture.Build<Item>()
                    .With(x => x.SellIn, sellIn)
                    .With(x => x.Quality, quality)
                    .CreateMany()
                    .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(3));
        }

        [Fact]
        public void Given_QualityOfItemIsZero_When_QualityUpdateOccurs_Then_QualityIsNotDecreased()
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.SellIn, 7)
                .With(x => x.Quality, 0)
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(0));
        }

        [Fact]
        public void Given_AgedBrieItem_When_QualityUpdateOccurs_Then_QualityIsIncreased()
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.Name, "Aged Brie")
                .With(x => x.Quality, 4)
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(5));
        }

        [Fact]
        public void Given_AgedBrieItemPassesSellBy_When_QualityUpdateOccurs_Then_QualityIsIncreasedByTwo()
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.Name, "Aged Brie")
                .With(x => x.Quality, 4)
                .With(x => x.SellIn, 0)
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(6));
        }

        [Fact]
        public void Given_AgedBrieItemQualityReachesFifty_When_QualityUpdateOccurs_Then_QualityIsNotIncreased()
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.Name, "Aged Brie")
                .With(x => x.Quality, 50)
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(50));
        }

        [Fact]
        public void Given_LegendaryItem_When_QualityUpdateOccurs_Then_QualityDoesNotChange()
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.Name, "Sulfuras, Hand of Ragnaros")
                .With(x => x.Quality, 80)
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(80));
        }

        [Theory]
        [InlineData(10, 5)]
        [InlineData(8, 5)]
        public void Given_BackstagePassesHaveTenDaysOrLessToSell_When_QualityUpdateOccurs_Then_QualityIncreasesByTwo(int sellIn, int quality)
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.Name, "Backstage passes to a TAFKAL80ETC concert")
                .With(x => x.SellIn, sellIn)
                .With(x => x.Quality, quality)
                .CreateMany()
                .ToList();

            var expectedQuality = quality + 2;

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(expectedQuality));
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(3, 5)]
        [InlineData(1, 5)]
        public void Given_BackstagePassesHaveFiveDaysOrLessToSell_When_QualityUpdateOccurs_Then_QualityIncreasesByThree(int sellIn, int quality)
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.Name, "Backstage passes to a TAFKAL80ETC concert")
                .With(x => x.SellIn, sellIn)
                .With(x => x.Quality, quality)
                .CreateMany()
                .ToList();

            var expectedQuality = quality + 3;

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(expectedQuality));
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(-5, 30)]
        [InlineData(-100, 5)]
        public void Given_BackstagePassesHavePassedSellBy_When_QualityUpdateOccurs_Then_QualityDecreasesToZero(int sellIn, int quality)
        {
            // Arrange
            var items = _fixture.Build<Item>()
                .With(x => x.Name, "Backstage passes to a TAFKAL80ETC concert")
                .With(x => x.SellIn, sellIn)
                .With(x => x.Quality, quality)
                .CreateMany()
                .ToList();

            GildedRose sut = new(items);

            // Act
            sut.UpdateQuality();

            // Assert
            items.Should().AllSatisfy(i => i.Quality.Should().Be(0));
        }
    }
}
