using System.Net;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MotorolaRadioSystem.SystemTests.APITests
{
    public class When_Getting_Radio_Location
    {
        private string GetURIOfNewRadio()
        {
            return "/api/radios/100/location";
        }

        private string GetURIOfRadioWithLocation()
        {
            return "/api/radios/1/location";
        }

        private string GetURIOfRadioWithoutLocation()
        {
            return "/api/radios/10/location";
        }

        [Fact]
        public async Task With_Nonexistent_Radio_Is_Failure()
        {
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client.GetAsync(GetURIOfNewRadio());

                //Assert
                res.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task With_Radio_That_Does_Not_Have_Location_Is_Not_Found()
        {
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client.GetAsync(GetURIOfRadioWithoutLocation());

                //Assert
                res.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task With_Radio_That_Has_Location_Is_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client.GetAsync(GetURIOfRadioWithLocation());

                //Assert
                res.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task With_Radio_That_Has_Location_Returns_Expected_Location()
        {
            var expected = "CPH-4";
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client.GetAsync(GetURIOfRadioWithLocation());

                //Assert
                (await res.Content.ReadAsStringAsync()).ShouldBe(expected);
            }
        }
    }
}