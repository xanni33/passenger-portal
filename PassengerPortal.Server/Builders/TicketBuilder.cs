using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Server.Builders
{
    public class TicketBuilder : ITicketBuilder
    {
        private Passenger _passenger;
        private Connection _connection;
        private IDiscountStrategy _discountStrategy;
        private ITicketRepresentation _representation;
        private DateTime _departureDateTime;

        public TicketBuilder()
        {

        }

        public ITicketBuilder SetPassenger(Passenger passenger)
        {
            _passenger = passenger;
            return this;
        }

        public ITicketBuilder SetConnection(Connection connection)
        {
            _connection = connection;
            return this;
        }

        public ITicketBuilder SetDiscountStrategy(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
            return this;
        }

        public ITicketBuilder SetRepresentation(ITicketRepresentation representation)
        {
            _representation = representation;
            return this;
        }

        public ITicketBuilder SetDepartureDateTime(DateTime departureDateTime)
        {
            _departureDateTime = departureDateTime;
            return this;
        }

        public Ticket Build()
        {
            if (_passenger == null)
                throw new InvalidOperationException("Passenger not set.");
            if (_connection == null)
                throw new InvalidOperationException("Connection not set.");
            if (_discountStrategy == null)
                throw new InvalidOperationException("Discount strategy not set.");
            if (_representation == null)
                throw new InvalidOperationException("Ticket representation not set.");
            if (_departureDateTime == default)
                throw new InvalidOperationException("DepartureDateTime not set.");

            decimal totalPrice = _connection.TotalPrice;
            decimal finalPrice = _discountStrategy.ApplyDiscount(totalPrice);

            var ticket = new Ticket
            {
                BuyerId = _passenger.Id.ToString(),
                Routes = _connection.Routes,
                DepartureDateTime = _departureDateTime,
                PurchaseTime = DateTime.UtcNow,
                Price = finalPrice
            };

            return ticket;
        }
    }
}

