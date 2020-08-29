using System.Net;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MotorolaRadioSystem.SystemTests.APITests
{
    public class When_Setting_Radio_Location
    {
        private string GetURI()
        {
            return "/api/radio/10/location";
        }

        private string GetURIOfNonexistentRadio()
        {
            return "/api/radio/100/location";
        }

        private Encoder<string> GetEncoder()
        {
            return new Encoder<string>();
        }

        [Fact]
        public async Task With_Invalid_Location_For_Radio_Is_Not_Found()
        {
            //Arrange
            var data = "CPH-30";
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client
                    .PostAsync(GetURI(),
                        GetEncoder().JsonEncode(data));

                //Assert
                res.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
            }
        }

        [Fact]
        public async Task With_Nonexistent_Radio_Is_BadRequest()
        {
            //Arrange
            var data = "CPH-3";
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client
                    .PostAsync(GetURIOfNonexistentRadio(),
                        GetEncoder().JsonEncode(data));

                //Assert
                res.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task With_Valid_Location_For_Radio_Is_OK()
        {
            //Arrange
            var data = "CPH-3";
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client
                    .PostAsync(GetURI(),
                        GetEncoder().JsonEncode(data));

                //Assert
                res.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }
    }
}