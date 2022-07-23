using AutoFixture;
using Vrnz2.TestUtils.Conventions;

namespace Vrnz2.TestUtils
{
    public abstract class AbstractTest
    {
        protected Fixture Fixture { get; }
        
        protected AbstractTest()
        {
            Fixture = new Fixture();

            Fixture.Customize(new BaseConventions());
        }
    }
}
