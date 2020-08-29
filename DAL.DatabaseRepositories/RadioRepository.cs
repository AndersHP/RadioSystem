using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace DAL.DatabaseRepositories
{
    class RadioRepository : IRadioRepository
    {
        public Task<IEnumerable<Radio>> Get(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Radio>> Get(Expression<Func<Radio, bool>> filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Radio> Get(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> Create(Radio entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(Radio entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(Expression<Func<Radio, bool>> filter, Radio entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Expression<Func<Radio, bool>> filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(IEnumerable<Radio> collection, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
