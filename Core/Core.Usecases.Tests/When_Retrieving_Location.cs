using System.Threading;
using Core.Usecases.Tests.Fakes;
using Shouldly;
using Xunit;

namespace Core.Usecases.Tests
{
    public class When_Retrieving_Location
    {
        IRadioGod GetRadioGod() => new RadioGod(new FakeRadioRepository());

        [Fact]
        public async void With_Radio_That_Has_Location_Is_Successful()
        {
            var radiogod = GetRadioGod();
            var id = 1;
            var expectedLocation = "CPH-1";

            var res = await radiogod.RetrieveRadioLocation(id, new CancellationToken());

            res.Id.ShouldBe(expectedLocation);
        }

        [Fact]
        public async void With_Radio_Without_Location_Is_Null()
        {
            var radiogod = GetRadioGod();
            var id = 2;

            var res = await radiogod.RetrieveRadioLocation(id, new CancellationToken());

            res.ShouldBe(null);
        }
    }
}