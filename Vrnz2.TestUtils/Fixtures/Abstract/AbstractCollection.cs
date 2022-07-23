using Xunit;

namespace Vrnz2.TestUtils.Fixtures.Abstract
{
    public abstract class AbstractCollection<TFixture>
        : ICollectionFixture<TFixture>
            where TFixture : class
    {
    }
}
