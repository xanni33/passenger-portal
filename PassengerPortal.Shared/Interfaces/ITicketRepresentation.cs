using PassengerPortal.Shared.Models;

namespace PassengerPortal.Shared.Interfaces
{
    public interface ITicketRepresentation
    {
        string Represent(Ticket ticket);
    }
}