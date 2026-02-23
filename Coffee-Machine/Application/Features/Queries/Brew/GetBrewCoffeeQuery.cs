using Coffee_Machine.Application.Common;
using MediatR;

namespace Coffee_Machine.Application.Features.Queries.Brew
{
    public record GetBrewCoffeeQuery() : IRequest<APIResponse<CoffeeStatus>>;
}
