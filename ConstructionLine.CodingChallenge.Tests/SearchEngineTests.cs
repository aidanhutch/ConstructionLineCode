using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void SearchEngineTest_NullSizesSearch_ReturnsArgumentNullException()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = null
            };

            Assert.Throws<ArgumentNullException>(() =>
            {
                searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void SearchEngineTest_NullSearchCriteria_ThrowArgumentNullException()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            var searchEngine = new SearchEngine(shirts);

            Assert.Throws<ArgumentNullException>(() =>
            {
                searchEngine.Search(null);
            });
        }

        [Test]
        public void SearchEngineTest_NoSearchData_ThrowArgumentNullException()
        {

            var searchEngine = new SearchEngine(null);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black },
                Sizes = new List<Size>() { Size.Large }
            };

            Assert.Throws<ArgumentNullException>(() =>
            {
                searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void SearchEngineTest_CanFindRedShirtsOnly()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngineTest_BlackShirtsCriteria_ReturnOnlyLargeSizes()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black },
                Sizes = new List<Size>() { Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngineTest_SmallSizesAllCriteria_ReturnSmallSizes()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Black, Color.Blue, Color.Yellow, Color.White },
                Sizes = new List<Size> { Size.Large, Size.Medium, Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

       
    }
}
