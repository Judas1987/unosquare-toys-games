using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using Moq;
using ToysGames.API.Repositories;
using ToysGames.Data;
using ToysGames.Data.Models;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ToysGames.UnitTesting.API
{
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public AsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerator<T> GetEnumerator() =>
            new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator) =>
            this.enumerator = enumerator ?? throw new ArgumentNullException();

        public T Current => enumerator.Current;

        public void Dispose()
        {
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> MoveNext(CancellationToken cancellationToken) =>
            Task.FromResult(enumerator.MoveNext());

        public ValueTask<bool> MoveNextAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class BaseRepositoryUnitTest
    {
        [Fact]
        public void CreateNewProductExpectSuccess()
        {
            var data = new List<Product>()
            {
                new Product(new Guid(), null, null, null, null, null),
                new Product(new Guid(), null, null, null, null, null),
                new Product(new Guid(), null, null, null, null, null)
            }.AsQueryable();

            var mockedContext = new Mock<ProductContext>();
            var mockedDbSet = new Mock<DbSet<Product>>();

            mockedContext.Setup(itm => itm.SaveChanges())
                .Callback(() => {

                    (data as List<Product>).Add(new Product(new Guid(), null, null, null, null, null));

                });

            mockedDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockedContext.Setup(context => context.Set<Product>())
                .Returns(() => mockedDbSet.Object);
            var productsRepository = new BaseRepository<Product>(mockedContext.Object);

            productsRepository.Insert(new Product(new Guid(), "Cool Barby",
                "This is a cool barby I found on the internet.", 3, "Mattel", 234));

            var result = mockedContext.Object.SaveChanges();


            var bla = productsRepository.Get();

            Console.WriteLine("Hello world");
        }
    }
}