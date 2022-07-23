using FluentAssertions;
using Vrnz2.TestUtils.Tests.Fixtures;
using Xunit;

namespace Vrnz2.TestUtils.Tests
{
    [Collection(MyTestCollections.MyTestCollection)]
    public class CollectionTest01
        : AbstractCollectionTest<MyTextFixture>
    {
        #region Constructors

        public CollectionTest01(MyTextFixture collectionFixture)
            : base(collectionFixture)
        {
        }

        #endregion

        #region Methods

        //[Fact]
        public void TestCollection01()
        {
            Run(() =>
            {
                // Arrange
                var value01 = 1;
                var value02 = 2;

                // Act
                var response = value01 + value02;

                // Assert
                response.Should().Be(3);
            });
        }

        #endregion
    }
}
