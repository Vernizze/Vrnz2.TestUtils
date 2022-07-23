using System;
using Vrnz2.TestUtils.Fixtures.Abstract;

namespace Vrnz2.TestUtils
{
    public abstract class AbstractCollectionTest<TCollectionFixture>
        : AbstractTest
            where TCollectionFixture : AbstractFixture
    {
        #region Variables

        protected TCollectionFixture _collectionFixture;

        #endregion

        #region Constructors

        protected AbstractCollectionTest(TCollectionFixture collectionFixture)
            => _collectionFixture = collectionFixture;

        #endregion

        #region Methods

        public void Run(Action action) 
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _collectionFixture.Dispose();

                throw;
            }
        }

        #endregion
    }
}
