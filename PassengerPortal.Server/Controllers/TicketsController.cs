using Microsoft.AspNetCore.Mvc;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;
using PassengerPortal.Shared.Representations;
using PassengerPortal.Server.Services;
using PassengerPortal.Shared.Strategies;
using PassengerPortal.Shared.Representations;
using PassengerPortal.Shared.Strategies;

namespace PassengerPortal.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketRepository _ticketRepository;

        public TicketsController(ITicketService ticketService,ITicketRepository ticketRepo)
        {
            _ticketService = ticketService;
            _ticketRepository = ticketRepo;

        }

        
        [HttpPost("purchase")]
        public IActionResult PurchaseTicket([FromBody] PurchaseTicketRequest request)
        {
            IDiscountStrategy discountStrategy = request.SelectedDiscountStrategy switch
            {
                "Percentage" => new PercentageDiscountStrategy(10),
                "FixedAmount" => new FixedAmountDiscountStrategy(20),
                _ => new NoDiscountStrategy(),
            };
            
            ITicketRepresentation representation = request.SelectedTicketRepresentation switch
            {
                "PDF"   => new PdfRepresentation(_ticketRepository), 
                "Email" => new EmailRepresentation(_ticketRepository),
                "Print" => new PrintRepresentation(_ticketRepository),
                _ => new EmailRepresentation(_ticketRepository),
            };


            bool success = _ticketService.PurchaseTicket(
                request.RouteIds,
                request.DepartureDateTime,
                request.BuyerId,
                out string representationMessage,
                out int newTicketId, 
                discountStrategy,
                representation
            );

            if (success)
            {
                return Ok(new PurchaseTicketResponse
                {
                    TicketId = newTicketId,
                    TicketRepresentation = representationMessage
                });
            }
            else
            {
                return BadRequest(new { Message = representationMessage });
            }
        }


        [HttpGet("{id}/representation/{type}")]
        public IActionResult GetTicketRepresentation(int id, string type)
        {
            var ticket = _ticketService.GetTicketById(id);
            if (ticket == null)
                return NotFound();

            
            ITicketRepresentation representation = type.ToLower() switch
            {
                "pdf"   => new PdfRepresentation(_ticketRepository),
                "print" => new PrintRepresentation(_ticketRepository),
                _       => new EmailRepresentation(_ticketRepository),
            };
            
            var report = new ConcreteTicketReport(representation);
            string ticketRepresentation = report.Represent(ticket);

            return Ok(new PurchaseTicketResponse
            {
                TicketId = ticket.Id,
                TicketRepresentation = ticketRepresentation
            });
        }

        public class PurchaseTicketRequest
        {
            public List<int> RouteIds { get; set; }
            public DateTime DepartureDateTime { get; set; }
            public string BuyerId { get; set; }
            
            public string SelectedDiscountStrategy { get; set; } =
                "NoDiscount"; 

            public string SelectedTicketRepresentation { get; set; } = "Email"; 
        }
        
        public class PurchaseTicketResponse
        {
            public int TicketId { get; set; }
            public string TicketRepresentation { get; set; }
        }


        public class ErrorResponse
        {
            public string Message { get; set; }
        }
    }
}
