using FluentAssertions;
using Vrnz2.TestUtils.Tests.Fixtures;
using Xunit;

namespace Vrnz2.TestUtils.Tests
{
    [Collection(MyTestCollections.MyTestCollection)]
    public class CollectionTest02
        : AbstractCollectionTest<MyTextFixture>
    {
        #region Constructors

        public CollectionTest02(MyTextFixture collectionFixture)
            : base(collectionFixture)
        {
        }

        #endregion

        #region Methods

        //[Fact]
        public void TestCollection02()
        {
            Run(() =>
            {
                // Arrange
                var value01 = 3;
                var value02 = 4;

                // Act
                var response = value01 + value02;

                // Assert
                response.Should().Be(7);
            });
        }

        #endregion
    }
}
