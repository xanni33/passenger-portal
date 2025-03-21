using System.Text;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Shared.Representations;

public class EmailRepresentation : ITicketRepresentation
{
    private readonly ITicketRepository _ticketRepository;
    
    public EmailRepresentation(ITicketRepository ticketRepo = null)
    {
        _ticketRepository = ticketRepo;
    }

    public string Represent(Ticket ticket)
    {
        if (ticket.Routes == null || ticket.Routes.Count == 0)
        {
            return $"EMAIL TICKET\n" +
                   $"User: {ticket.BuyerId}\n" +
                   $"Brak tras w tym bilecie.\n" +
                   $"Price: {ticket.Price:C}\n";
        }
        
        var realDeparture = _ticketRepository.GetRealDepartureDateTime(ticket.Id);
        
        if (realDeparture.HasValue && realDeparture.Value != DateTime.MinValue)
        {
            ticket.DepartureDateTime = realDeparture.Value;
        }
        
        var builder = new StringBuilder();

        builder.AppendLine("EMAIL TICKET");
        builder.AppendLine($"User: {ticket.BuyerId}");
        builder.AppendLine($"Price: {ticket.Price:C}");
        
        if (ticket.DepartureDateTime == DateTime.MinValue)
        {
            builder.AppendLine("Departure: -∞ (lub brak)");
        }
        else
        {
            var depLocal = ticket.DepartureDateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            builder.AppendLine($"Departure: {depLocal}");
        }
        
        builder.AppendLine("Routes:");
        foreach (var route in ticket.Routes)
        {
            var startName = route.StartStation?.Name ?? "Brak stacji początkowej";
            var endName   = route.EndStation?.Name   ?? "Brak stacji końcowej";
            builder.AppendLine($"  {startName} -> {endName}");
        }

        return builder.ToString();
    }
}
