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
        public GetBrewCoffeeQueryHandler(IDateTimeProvider dateTimeProvider, IBrewCounter brewCounter)
        {
            _dateTimeProvider = dateTimeProvider;
            _brewCounter = brewCounter; 
        }

        public Task<APIResponse<CoffeeStatus>> Handle(GetBrewCoffeeQuery request, CancellationToken cancellationToken)
        {
            int callNumber = _brewCounter.IncrementValue();

            if (_dateTimeProvider.Today.Month == 4 && _dateTimeProvider.Today.Day == 1)
            {
                return Task.FromResult<APIResponse<CoffeeStatus>>(null);
            }

            //on 5th call machine will be unavailable
            if (callNumber % 5 == 0)
                throw new MachineUnavailableException();

            var response = new APIResponse<CoffeeStatus>
            {
                Message = "Your piping hot coffee is ready",
                Prepared = _dateTimeProvider.Now.ToString("yyyy-MM-ddTHH:mm:ssK"),
                Data = new CoffeeStatus
                {
                    IsOutOfCoffee = false,
                }
            };

            return Task.FromResult(response);
        }
    }
}
