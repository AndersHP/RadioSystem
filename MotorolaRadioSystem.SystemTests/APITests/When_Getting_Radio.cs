using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MotorolaRadioSystem.SystemTests.APITests
{
    public class When_Getting_Radio
    {
        private string GetUri()
        {
            return "/api/radios/";
        }

        [Fact]
        public async Task With_Existing_Radio_Is_Successful()
        {
            //Arrange
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client.GetAsync(GetUri() + 1);

                //Assert
                res.IsSuccessStatusCode.ShouldBe(true);
            }
        }

        [Fact]
        public async Task With_Existing_Radio_Returns_Expected_Radio()
        {
            //Arrange
            //TODO: Could change expected to match specification by implementing two types for returning depending on the existence of Location
            var expected =
                "{\"id\":10,\"alias\":\"Radio10\",\"allowedLocations\":[{\"id\":\"CPH-1\"},{\"id\":\"CPH-2\"},{\"id\":\"CPH-3\"},{\"id\":\"CPH-4\"},{\"id\":\"CPH-5\"}],\"currentLocation\":null}";
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client.GetAsync(GetUri() + 10);

                //Assert
                (await res.Content.ReadAsStringAsync()).ShouldBe(expected);
            }
        }

        [Fact]
        public async Task With_Invalid_Radio_Id_Is_Unsuccessful()
        {
            //Arrange
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client.GetAsync(GetUri() + "abcd");

                //Assert
                res.IsSuccessStatusCode.ShouldBe(false);
            }
        }

        [Fact]
        public async Task With_Nonexistent_Radio_Is_Unsuccessful()
        {
            //Arrange
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client.GetAsync(GetUri() + 1000);

                //Assert
                res.IsSuccessStatusCode.ShouldBe(false);
            }
        }
    }
}