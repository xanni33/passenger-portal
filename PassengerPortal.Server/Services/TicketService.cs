
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;
using PassengerPortal.Server.Builders;
using PassengerPortal.Server.Data;
namespace PassengerPortal.Server.Services;

    public class TicketService : ITicketService
    {
        private readonly IRouteRepository _routeRepo;
        private readonly ITicketRepository _ticketRepo;
        private readonly ApplicationDbContext _context;

        public TicketService(
            IRouteRepository routeRepo,
            ITicketRepository ticketRepo,
            ApplicationDbContext context)
        {
            _routeRepo = routeRepo;
            _ticketRepo = ticketRepo;
            _context = context;
        }
public bool PurchaseTicket(
    List<int> routeIds,
    DateTime departureDateTime,
    string buyerId,
    out string representationMessage,
    out int newTicketId,  
    IDiscountStrategy discountStrategy,
    ITicketRepresentation representation)
{
    representationMessage = string.Empty;
    newTicketId = 0; 

    using (var transaction = _context.Database.BeginTransaction())
    {
        try
        {
            var routes = _routeRepo.GetAll()
                .Where(r => routeIds.Contains(r.Id))
                .ToList();

            if (routes.Count != routeIds.Count)
            {
                representationMessage = "Jedna lub więcej tras nie istnieje.";
                return false;
            }
            
            foreach (var route in routes)
            {
                var soldTickets = _ticketRepo.GetSoldTickets(route.Id, departureDateTime);
                if (soldTickets >= route.AvailableSeats)
                {
                    representationMessage = $"Brak dostępnych miejsc na trasie {route.StartStation.Name} → {route.EndStation.Name}.";
                    return false;
                }
            }
            
            var passenger = new Passenger
            {
                Id = int.Parse(buyerId),
                Username = "Użytkownik"
            };
            
            var connection = new Connection
            {
                Routes = routes
            };
            
            var ticketBuilder = new TicketBuilder();
            var ticket = ticketBuilder
                .SetPassenger(passenger)
                .SetConnection(connection)
                .SetDiscountStrategy(discountStrategy)
                .SetRepresentation(representation)
                .SetDepartureDateTime(departureDateTime)
                .Build();
            
            _ticketRepo.Add(ticket);
            _ticketRepo.Save();
            
            newTicketId = ticket.Id;  
            
            foreach (var route in routes)
            {
                route.AvailableSeats -= 1;
                _routeRepo.Update(route);
            }
            _routeRepo.Save();
            
            transaction.Commit();
            
            representationMessage = representation.Represent(ticket);
            
            return true;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            representationMessage = $"Wystąpił błąd podczas zakupu biletu: {ex.Message}";
            return false;
        }
    }
}
    public Ticket GetTicketById(int id)
        {
            return _ticketRepo.GetById(id);
        }
    }


        

       
