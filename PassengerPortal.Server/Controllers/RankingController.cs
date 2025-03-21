using Microsoft.AspNetCore.Mvc;
using PassengerPortal.Server.Services;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly RankingService _rankingService;

        public RankingController(RankingService rankingService)
        {
            _rankingService = rankingService;
        }

        [HttpGet]
        public IActionResult GetRankings()
        {
            return Ok(_rankingService.GetRankings());
        }

        [HttpPost("vote/{trainId}")]
        public IActionResult Vote(int trainId)
        {
            _rankingService.Vote(trainId);
            return Ok();
        }
    }
}