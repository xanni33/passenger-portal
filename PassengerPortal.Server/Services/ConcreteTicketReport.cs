using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Server.Services;

public class ConcreteTicketReport : TicketReport
{
    public ConcreteTicketReport(ITicketRepresentation representation)
        : base(representation)
    {
    }

    public override string Represent(Ticket ticket)
    {
        return _representation.Represent(ticket);
    }
}