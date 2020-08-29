using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using static MotorolaRadioSystem.Controllers.RadiosController;

namespace MotorolaRadioSystem.SystemTests.APITests
{
    public class When_Creating_Radio
    {
        private string GetURI()
        {
            return "/api/radios/";
        }

        private Encoder<PostRadioRequest> GetEncoder()
        {
            return new Encoder<PostRadioRequest>();
        }

        [Fact]
        public async Task With_Taken_Id_Is_Unsuccessful()
        {
            //Arrange
            var id = "1";
            var data = new PostRadioRequest
            {
                alias = "radio100",
                allowedLocations = new List<string>
                {
                    "CPH-1",
                    "CPH-2",
                    "CPH-3"
                }
            };
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client
                    .PostAsync(GetURI() + id,
                        GetEncoder().JsonEncode(data));

                //Assert
                res.IsSuccessStatusCode.ShouldBe(false);
            }
        }

        [Fact]
        public async Task With_Valid_Data_Is_OK()
        {
            //Arrange
            var id = "100";
            var data = new PostRadioRequest
            {
                alias = "radio100",
                allowedLocations = new List<string>
                {
                    "CPH-1",
                    "CPH-2",
                    "CPH-3"
                }
            };
            using (var client = new TestClientProvider().Client)
            {
                //Act
                var res = await client
                    .PostAsync(GetURI() + id,
                        GetEncoder().JsonEncode(data));

                //Assert
                res.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }
    }
}