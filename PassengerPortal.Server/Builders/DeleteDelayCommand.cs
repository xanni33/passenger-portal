using MediatR;

namespace PassengerPortal.Server.Builders;

public record DeleteDelayCommand(int DelayId) : IRequest<bool>;
