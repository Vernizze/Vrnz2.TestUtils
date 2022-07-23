using Vrnz2.TestUtils.Fixtures.Abstract;
using Xunit;

namespace Vrnz2.TestUtils.Tests.Fixtures
{
    [CollectionDefinition(MyTestCollections.MyTestCollection)]
    public class MyTestCollection
        : AbstractCollection<MyTextFixture>
    {
    }
}
