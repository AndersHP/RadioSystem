using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MotorolaRadioSystem.Controllers;
using Shouldly;
using Xunit;

namespace MotorolaRadioSystem.SystemTests.Scenarios
{
    public class Scenario1
    {
        private string GetUri()
        {
            return "/api/radio/";
        }

        private string GetLocation()
        {
            return "/location";
        }

        private Encoder<RadioController.PostRadioRequest> GetPostEncoder()
        {
            return new Encoder<RadioController.PostRadioRequest>();
        }

        private Encoder<string> GetLocationEncoder()
        {
            return new Encoder<string>();
        }

        [Fact]
        public async Task Scenario1_Is_Successful()
        {
            //Arrange
            var data1Id = 100;
            var step1Data = new RadioController.PostRadioRequest
            {
                alias = "radio100",
                allowedLocations = new List<string>
                {
                    "CPH-1",
                    "CPH-2"
                }
            };
            var data2Id = 101;
            var step2Data = new RadioController.PostRadioRequest
            {
                alias = "radio101",
                allowedLocations = new List<string>
                {
                    "CPH-1",
                    "CPH-2",
                    "CPH-3"
                }
            };
            var step3Data = "CPH-1";
            var step4Data = "CPH-3";
            var step5Data = "CPH-3";
            var step6expectation = "CPH-3";
            var step7expectation = "CPH-1";

            using (var client = new TestClientProvider().Client)
            {
                //Act
                var step1 = await client.PostAsync(GetUri() + data1Id,
                    GetPostEncoder().JsonEncode(step1Data));
                step1.StatusCode.ShouldBe(HttpStatusCode.OK);

                var step2 = await client.PostAsync(GetUri() + data2Id,
                    GetPostEncoder().JsonEncode(step2Data));
                step2.StatusCode.ShouldBe(HttpStatusCode.OK);

                var step3 = await client.PostAsync(GetUri() + data1Id + GetLocation(),
                    GetLocationEncoder().JsonEncode(step3Data));
                step3.StatusCode.ShouldBe(HttpStatusCode.OK);

                var step4 = await client.PostAsync(GetUri() + data2Id + GetLocation(),
                    GetLocationEncoder().JsonEncode(step4Data));
                step4.StatusCode.ShouldBe(HttpStatusCode.OK);

                var step5 = await client.PostAsync(GetUri() + data1Id + GetLocation(),
                    GetLocationEncoder().JsonEncode(step5Data));
                step5.StatusCode.ShouldBe(HttpStatusCode.Forbidden);

                var step6 = await client.GetAsync(GetUri() + data2Id + GetLocation());
                step6.StatusCode.ShouldBe(HttpStatusCode.OK);
                (await step6.Content.ReadAsStringAsync()).ShouldBe(step6expectation);

                var step7 = await client.GetAsync(GetUri() + data1Id + GetLocation());
                step7.StatusCode.ShouldBe(HttpStatusCode.OK);
                (await step7.Content.ReadAsStringAsync()).ShouldBe(step7expectation);
            }
        }
    }
}