using System;
using System.Collections.Generic;
using System.Threading;
using Core.Models;
using Core.Repositories;
using Core.Usecases.Tests.Fakes;
using Shouldly;
using Xunit;

namespace Core.Usecases.Tests
{
    //Everything would ideally have some testing similarly to this but due to time I've only done for the most important code
    public class When_Retrieving_Radio
    {
        IRadioGod GetRadioGod() => new RadioGod(new FakeRadioRepository());

        [Fact]
        public async void With_Existing_Id_Is_Not_Null()
        {
            //Arrange
            var radiogod = GetRadioGod();
            var id = 2;

            //Act
            var res = await radiogod.RetrieveRadio(id, new CancellationToken());

            //Assert
            res.ShouldNotBeNull();
        }

        [Fact]
        public async void With_Existing_Id_Is_Successful()
        {
            var radiogod = GetRadioGod();
            var id = 2;
            var expectedAlias = "Radio2";

            var res = await radiogod.RetrieveRadio(id, new CancellationToken());

            res.Alias.ShouldBe(expectedAlias);
        }
    }
}
