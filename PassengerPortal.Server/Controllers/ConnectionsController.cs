using Microsoft.AspNetCore.Mvc;
using PassengerPortal.Shared.Models;
using PassengerPortal.Shared.Interfaces;
using System;
using System.Collections.Generic;
using PassengerPortal.Server.Services;

namespace PassengerPortal.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectionsController : ControllerBase
    {
        private readonly ISearchStrategy _searchStrategy;
        private readonly IStationRepository _stationRepo;

        public ConnectionsController(ISearchStrategy searchStrategy, IStationRepository stationRepo)
        {
            _searchStrategy = searchStrategy;
            _stationRepo = stationRepo;
        }

        [HttpGet("search")]
        public IActionResult SearchConnections(
            [FromQuery] int startStationId,
            [FromQuery] int endStationId,
            [FromQuery] DateTime departureTime,
            [FromQuery] int maxResults = 5,
            [FromQuery] decimal? maximumPrice = null,
            [FromQuery] TrainType? trainType = null,
            [FromQuery] int? minimumSeats = null)
        {
            try
            {
                var startStation = _stationRepo.GetById(startStationId);
                var endStation = _stationRepo.GetById(endStationId);

                if (startStation == null || endStation == null)
                {
                    return BadRequest("Nieprawidłowe identyfikatory stacji.");
                }
                
                IConnectionFilter? filter = null;

                if (maximumPrice.HasValue || trainType.HasValue || minimumSeats.HasValue)
                {
                    var compositeFilter = new CompositeFilter();

                    if (maximumPrice.HasValue)
                    {
                        compositeFilter.AddFilter(new PriceFilter(maximumPrice.Value));
                    }

                    if (trainType.HasValue)
                    {
                        compositeFilter.AddFilter(new TrainTypeFilter(trainType.Value));
                    }

                    if (minimumSeats.HasValue)
                    {
                        compositeFilter.AddFilter(new AvailableSeatsFilter(minimumSeats.Value));
                    }

                    filter = compositeFilter;
                }

                var connections = _searchStrategy.SearchConnections(
                    startStation,
                    endStation,
                    departureTime,
                    maxResults,
                    filter
                );

                return Ok(connections);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Wystąpił błąd podczas wyszukiwania połączeń.");
            }
        }
    }
}

