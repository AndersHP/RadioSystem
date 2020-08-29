using System;
using System.Collections.Generic;
using System.Threading;
using Core.Models;
using Core.Usecases.Tests.Fakes;
using Shouldly;
using Xunit;

namespace Core.Usecases.Tests
{
    public class When_Setting_Location
    {
        IRadioGod GetRadioGod() => new RadioGod(new FakeRadioRepository());

        [Fact]
        public async void With_Valid_Location_For_Radio_Location_Is_Set()
        {
            //Arrange
            var radiogod = GetRadioGod();
            var id = 1;
            var location = new Location(){Id = "CPH-2"};

            //Act
            await radiogod.SetRadioLocation(id, location, new CancellationToken());

            //Assert
            var result = await radiogod.RetrieveRadio(id, new CancellationToken());
            result.CurrentLocation.Id.ShouldBe(location.Id);

        }

        [Fact]
        public async void With_Valid_Location_For_Radio_Location_Is_Changed()
        {
            //Arrange
            var radiogod = GetRadioGod();
            var id = 1;
            var location = new Location() { Id = "CPH-2" };
            var before = await radiogod.RetrieveRadio(id, new CancellationToken());
            var previousLocation = before.CurrentLocation;
            
            //Act
            await radiogod.SetRadioLocation(id, location, new CancellationToken());

            //Assert
            var result = await radiogod.RetrieveRadio(id, new CancellationToken());
            result.CurrentLocation.Id.ShouldNotBe(previousLocation.Id);

        }

        [Fact]
        public async void With_Invalid_Location_For_Radio_Location_Is_Unchanged()
        {
            //Arrange
            var radiogod = GetRadioGod();
            var id = 1;
            var location = new Location() { Id = "CPH-3" };
            var before = await radiogod.RetrieveRadio(id, new CancellationToken());
            var previousLocation = before.CurrentLocation;

            //Act
            try
            {
                await radiogod.SetRadioLocation(id, location, new CancellationToken());
            }
            catch (Exception e)
            {
            }

            //Assert
            var result = await radiogod.RetrieveRadio(id, new CancellationToken());
            result.CurrentLocation.Id.ShouldBe(previousLocation.Id);
        }

        [Fact]
        public async void With_Nonexistent_Radio_Throws()
        {
            //Arrange
            var radiogod = GetRadioGod();
            var id = 100;
            var location = new Location() { Id = "CPH-3" };

            await radiogod.SetRadioLocation(id, location, new CancellationToken()).ShouldThrowAsync<KeyNotFoundException>();

        }
    }
}