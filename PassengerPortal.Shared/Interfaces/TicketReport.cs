using PassengerPortal.Shared.Models;

namespace PassengerPortal.Shared.Interfaces;

public abstract class TicketReport
{
    protected ITicketRepresentation _representation;

    protected TicketReport(ITicketRepresentation representation)
    {
        _representation = representation;
    }

    public abstract string Represent(Ticket ticket);
}