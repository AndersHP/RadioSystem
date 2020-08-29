using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace Core.Usecases
{
    //This could/should be split into multiple classes with their own single usecase to avoid a godclass
    public class RadioGod : IRadioGod
    {
        private readonly IRadioRepository _radioRepository;

        public RadioGod(IRadioRepository radioRepository)
        {
            _radioRepository = radioRepository;
        }
        public async Task<IEnumerable<Radio>> RetrieveAllRadios(CancellationToken token)
        {
            return await _radioRepository.Get(token);
        }

        public async Task<Radio> RetrieveRadio(int id, CancellationToken token)
        {
            return await _radioRepository.Get(id, token);
        }

        public async Task<Location> RetrieveRadioLocation(int id, CancellationToken token)
        {
            var radio = await _radioRepository.Get(id, token);
            return radio.CurrentLocation;
        }

        public async Task<int> CreateRadio(Radio radio, CancellationToken token)
        {
            return await _radioRepository.Create(radio, token);
        }

        //Using the return value to indicate success/failure would be preferred over Exception
        //Ideally I'd use a choice type as used in functional programming
        public async Task SetRadioLocation(int id, Location location, CancellationToken token)
        {
            var radio = await _radioRepository.Get(id, token);

            //Convert to string to get deep comparison instead of identity
            if (radio.AllowedLocations.Select(loc => loc.Id).Contains(location.Id))
            {
                radio.CurrentLocation = location;
                await _radioRepository.Update(radio, token);
                return;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"radio {id}, does not allow given location");
            }
        }
    }
}
