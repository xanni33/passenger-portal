using MediatR;
using PassengerPortal.Shared.Models;
using System.Collections.Generic;

namespace PassengerPortal.Server.Builders;

public class GetDelaysQuery : IRequest<List<Delay>>
{
}