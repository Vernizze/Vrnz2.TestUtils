using AutoFixture;
using MongoDB.Bson;

namespace Vrnz2.TestUtils.Conventions
{
    internal class BaseConventions
        : CompositeCustomization
    {
        public BaseConventions() :
            base(new MongoObjectIdCustomization())
        {

        }

        private class MongoObjectIdCustomization
            : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(ObjectId.GenerateNewId);
            }
        }
    }
}
