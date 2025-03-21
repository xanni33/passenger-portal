using MediatR;
using PassengerPortal.Server.Builders;
using PassengerPortal.Server.Data;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Server.Services.Handlers;

public class CreateDelayHandler : IRequestHandler<CreateDelayCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateDelayHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDelayCommand request, CancellationToken cancellationToken)
    {
        var delay = new Delay
        {
            TrainNumber = request.TrainNumber,
            Route = request.Route,
            DepartureTime = request.DepartureTime,
            DelayInMinutes = request.DelayInMinutes
        };

        _context.Delays.Add(delay);
        await _context.SaveChangesAsync(cancellationToken);
        //test
        Console.WriteLine("Dodano opóźnienie: " + delay.TrainNumber);
        return delay.Id;
    }
}
