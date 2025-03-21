using PassengerPortal.Shared.Models;

namespace PassengerPortal.Shared.Interfaces;

public interface IRankingObserver
{
    void UpdateRanking(List<TrainRanking> rankings);
}
