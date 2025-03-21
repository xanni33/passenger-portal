
using MediatR;

namespace PassengerPortal.Server.Builders;

public record CreateDelayCommand(string TrainNumber, string Route, DateTime DepartureTime, int DelayInMinutes) : IRequest<int>;
