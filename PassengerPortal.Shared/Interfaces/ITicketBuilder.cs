using PassengerPortal.Shared.Models;

namespace PassengerPortal.Shared.Interfaces;

public interface ITicketBuilder
{
    ITicketBuilder SetPassenger(Passenger passenger);
    ITicketBuilder SetConnection(Connection connection);
    ITicketBuilder SetDiscountStrategy(IDiscountStrategy discountStrategy);
    ITicketBuilder SetRepresentation(ITicketRepresentation representation);
    ITicketBuilder SetDepartureDateTime(DateTime departureDateTime);
    Ticket Build();
}