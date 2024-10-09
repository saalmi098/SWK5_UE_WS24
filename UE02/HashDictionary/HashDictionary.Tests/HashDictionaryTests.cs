using FluentAssertions;
using HashDictionary.Impl;

namespace HashDictionary.Tests
{
    public class HashDictionaryTests
    {
        [Fact]
        public void AddAndIndexerGetterAreConsistent()
        {
            // prepare
            var dict = new HashDictionary<int, int>();

            // act
            dict.Add(1, 10);

            // assert
            //Assert.Equal(10, dict[1]);
            dict[1].Should().Be(10); // Fluent Assertions (Nuget Package)

            dict.Add(3, 30);
            //Assert.Equal(10, dict[1]);
            //Assert.Equal(30, dict[3]);
            dict[1].Should().Be(10);
            dict[3].Should().Be(30);

            dict.Add(2, 20);
            //Assert.Equal(10, dict[1]);
            //Assert.Equal(20, dict[2]);
            //Assert.Equal(30, dict[3]);
            dict[1].Should().Be(10);
            dict[2].Should().Be(20);
            dict[3].Should().Be(30);
        }

        [Fact]
        public void IndexerGetterAndSetterAreConsistent()
        {
            var dict = new HashDictionary<int, int>();
            dict[1] = 10;
            Assert.Equal(10, dict[1]);

            dict[2] = 20;
            Assert.Equal(10, dict[1]);
            Assert.Equal(20, dict[2]);

            dict[3] = 30;
            Assert.Equal(10, dict[1]);
            Assert.Equal(20, dict[2]);
            Assert.Equal(30, dict[3]);
        }

        [Fact]
        public void ArgumentException_WhenItemAddedTwice()
        {
            var dict = new HashDictionary<int, int>();
            dict.Add(1, 10);
            dict.Add(2, 20);

            //Assert.Throws<ArgumentException>(() => dict.Add(2, 30));
            Action act = () => dict.Add(2, 30);
            act.Should().Throw<ArgumentException>(); // Fluent Assertions
        }

        [Theory]
        [InlineData(new[] { 10 }, 1)]
        [InlineData(new[] { 10, 20 }, 2)]
        [InlineData(new[] { 10, 20, 30 }, 3)]
        public void Theory_CountPropertyIsOk_WhenAddingItems(IEnumerable<int> list, int expected)
        {
            var dict = new HashDictionary<int, int>();
            int i = 0;
            foreach (var item in list)
            {
                dict.Add(i++, item);
            }

            Assert.Equal(expected, dict.Count);
        }
    }
}