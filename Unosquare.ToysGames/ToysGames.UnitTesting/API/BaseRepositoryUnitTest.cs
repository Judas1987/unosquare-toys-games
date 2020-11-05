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
   
    }
}