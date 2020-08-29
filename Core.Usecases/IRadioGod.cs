using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Usecases
{
    public interface IRadioGod
    {
        Task<IEnumerable<Radio>> RetrieveAllRadios(CancellationToken token);
        Task<Radio> RetrieveRadio(int id, CancellationToken token);
        Task<Location> RetrieveRadioLocation(int id, CancellationToken token);
        Task<int> CreateRadio(Radio radio, CancellationToken token);
        Task SetRadioLocation(int id, Location location, CancellationToken token);
    }
}