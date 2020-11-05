using Moq;
using ToysGames.API.Workers;
using ToysGames.Data;
using Xunit;

namespace ToysGames.UnitTesting.API
{
    public class UnitOfWorkUnitTesting
    {
        /// <summary>
        /// This method tests the creation of the unit of work class instance.
        /// </summary>
        [Fact]
        public void UnitOfWorkCreateInstanceSuccessExpected()
        {
            var mockedContext = new Mock<ProductContext>();
            var unitOfWork = new UnitOfWork(mockedContext.Object);

            Assert.NotNull(unitOfWork);
            Assert.NotNull(unitOfWork.Products);
        }

        /// <summary>
        /// This method verifies that the commit action calls the SaveChanges() method of EF once and only once. 
        /// </summary>
        [Fact]
        public void UnitOfWorkCommitIsCalledOnce()
        {
            var mockedContext = new Mock<ProductContext>();
            var unitOfWork = new UnitOfWork(mockedContext.Object);

            unitOfWork.Commit();

            mockedContext.Verify(itm => itm.SaveChanges(), Times.Once);
        }
    }
}