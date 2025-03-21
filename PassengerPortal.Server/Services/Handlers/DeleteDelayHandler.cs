using MediatR;
using PassengerPortal.Server.Builders;
using PassengerPortal.Server.Data;

namespace PassengerPortal.Server.Services.Handlers;

public class DeleteDelayHandler : IRequestHandler<DeleteDelayCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteDelayHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteDelayCommand request, CancellationToken cancellationToken)
    {
        var delay = await _context.Delays.FindAsync(request.DelayId);
        if (delay == null)
            return false;

        _context.Delays.Remove(delay);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
