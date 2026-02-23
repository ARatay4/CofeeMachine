using Coffee_Machine.Application.Exceptions;
using Coffee_Machine.Application.Features.Queries.Brew;
using Coffee_Machine.Application.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoffeeMachine.Tests
{
    public class GetBrewCoffeeHandlerTests
    {
        [Theory]
        [InlineData("NormalDay")]       // First call normal
        [InlineData("FifthCall")]      // 5th call unavailable
        [InlineData("April1st")]       // April 1st teapot
        [InlineData("HotWeather")]       // Hot weather
        public async Task GetBrewCoffeeQueryHandler_AllScenarios(string scenario)
        {
            // Arrange
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBrewCounter = new Mock<IBrewCounter>();
            var mockWeatherService = new Mock<IWeatherService>();

            mockWeatherService.Setup(s => s.GetCurrentTemperatureAsync()).ReturnsAsync(32);

            mockDateTimeProvider.Setup(d => d.Now).Returns(new DateTime(2026, 2, 23, 10, 30, 0));

            if (scenario == "HotWeather")
                mockWeatherService.Setup(s => s.GetCurrentTemperatureAsync()).ReturnsAsync(32); // hot weather
            else
                mockWeatherService.Setup(s => s.GetCurrentTemperatureAsync()).ReturnsAsync(25); // normal temperature
            

            if (scenario == "April1st")
                mockDateTimeProvider.Setup(d => d.Today).Returns(new DateTime(2026, 4, 1));
            else
                mockDateTimeProvider.Setup(d => d.Today).Returns(new DateTime(2026, 2, 23));


            if (scenario == "FifthCall")
                mockBrewCounter.Setup(c => c.IncrementValue()).Returns(5);
            else
                mockBrewCounter.Setup(c => c.IncrementValue()).Returns(1);

            var handler = new GetBrewCoffeeQueryHandler(mockDateTimeProvider.Object, mockBrewCounter.Object, mockWeatherService.Object);

            // Act + Assert
            if (scenario == "FifthCall")
            {
                await Assert.ThrowsAsync<MachineUnavailableException>(() =>
                    handler.Handle(new GetBrewCoffeeQuery(), CancellationToken.None));
            }
            else
            {
                var result = await handler.Handle(new GetBrewCoffeeQuery(), CancellationToken.None);

                if (scenario == "April1st")
                {
                    result.Should().BeNull();
                }
                else
                {
                    if (scenario == "HotWeather")
                    {
                        result.Should().NotBeNull();
                        result.Message.Should().Be("Your refreshing iced coffee is ready");
                        result.Data.IsOutOfCoffee.Should().BeFalse();
                    }
                    else
                    {
                        result.Should().NotBeNull();
                        result.Message.Should().Be("Your piping hot coffee is ready");
                        result.Data.IsOutOfCoffee.Should().BeFalse();
                    }
                }
            }
        }
    }
}
