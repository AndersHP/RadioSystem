using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace DAL.InMemoryRepositories
{
    public class InMemoryRadioRepository : IRadioRepository
    {
        private readonly Dictionary<int, Radio> _radios = new Dictionary<int, Radio>();

        public InMemoryRadioRepository()
        {
            var locations = new List<Location>()
            {
                new Location(){Id = "CPH-1"},
                new Location(){Id = "CPH-2"},
                new Location(){Id = "CPH-3"},
                new Location(){Id = "CPH-4"},
                new Location(){Id = "CPH-5"}
            };

            for (var i = 0; i < 20; i++)
            {
                _radios.Add(i, new Radio(){Alias = $"Radio{i}", Id = i, AllowedLocations = locations});
            }
            _radios[1].CurrentLocation = new Location(){Id = "CPH-4"};
        }

        public Task<IEnumerable<Radio>> Get(CancellationToken cancellationToken)
        {
            return Task.FromResult(_radios.Values.AsEnumerable());
        }

        public Task<IEnumerable<Radio>> Get(Expression<Func<Radio, bool>> filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Radio> Get(int id, CancellationToken cancellationToken)
        {
            var radioExist = _radios.TryGetValue(id, out var res);

            if (radioExist) return Task.FromResult(res);
            else
            {
                throw new KeyNotFoundException($"Radio with ID: {id} not found");
            }
        }

        public Task<int> Create(Radio radio, CancellationToken cancellationToken)
        {
            _radios.Add(radio.Id, radio);
            return Task.FromResult(radio.Id);
        }

        public Task Update(Radio radio, CancellationToken cancellationToken)
        {
            if (_radios.ContainsKey(radio.Id))
            {
                _radios[radio.Id] = radio;
                return Task.CompletedTask;
            }
            else
            {
                throw new Exception($"radio with ID: {radio.Id} doesn't exist");
            }
        }

        public Task Update(Expression<Func<Radio, bool>> filter, Radio entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, CancellationToken cancellationToken)
        {
            if (_radios.ContainsKey(id))
            {
                _radios.Remove(id);
            }

            return Task.CompletedTask;
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
