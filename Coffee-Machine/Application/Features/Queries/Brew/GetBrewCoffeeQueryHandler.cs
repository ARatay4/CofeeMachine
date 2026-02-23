using Coffee_Machine.Application.Common;
using Coffee_Machine.Application.Exceptions;
using Coffee_Machine.Application.Interface;
using MediatR;

namespace Coffee_Machine.Application.Features.Queries.Brew
{
    public class GetBrewCoffeeQueryHandler : IRequestHandler<GetBrewCoffeeQuery, APIResponse<CoffeeStatus>>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IBrewCounter _brewCounter;
        private readonly IWeatherService _weatherService;
        public GetBrewCoffeeQueryHandler(IDateTimeProvider dateTimeProvider, IBrewCounter brewCounter, IWeatherService weatherService)
        {
            _dateTimeProvider = dateTimeProvider;
            _brewCounter = brewCounter; 
            _weatherService = weatherService;
        }

        public async Task<APIResponse<CoffeeStatus>> Handle(GetBrewCoffeeQuery request, CancellationToken cancellationToken)
        {
            int callNumber = _brewCounter.IncrementValue();

            if (_dateTimeProvider.Today.Month == 4 && _dateTimeProvider.Today.Day == 1)
            {
                return null;
            }

            //on 5th call machine will be unavailable
            if (callNumber % 5 == 0)
                throw new MachineUnavailableException();

            string message = "Your piping hot coffee is ready";

            // IP-based weather check
            double temp = await _weatherService.GetCurrentTemperatureAsync();
            if (temp > 30)
                message = "Your refreshing iced coffee is ready";

            var response = new APIResponse<CoffeeStatus>
            {
                Message = message,
                Prepared = _dateTimeProvider.Now.ToString("yyyy-MM-ddTHH:mm:ssK"),
                Data = new CoffeeStatus
                {
                    IsOutOfCoffee = false,
                }
            };

            return response;
        }
    }
}
