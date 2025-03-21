
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Shared.Models;

namespace PassengerPortal.Client.Services
{
    public class RankingService : ISubject
    {
        private readonly List<IObserver> _observers = new();
        private readonly List<TrainRanking> _rankings;

        public RankingService()
        {
            //  (statyczne zdjęcia pociągów)
            _rankings = new List<TrainRanking>
            {
                new TrainRanking { TrainId = 1, Name = "Lokomotywa czeskich drahów", Votes = 0, ImagePath = "/images/trains/drahy.jpg" },
                new TrainRanking { TrainId = 2, Name = "E4MSU", Votes = 0, ImagePath = "/images/trains/E4MSU.jpg" },
                new TrainRanking { TrainId = 3, Name = "EP05", Votes = 0, ImagePath = "/images/trains/ep05.jpg" },
                new TrainRanking { TrainId = 1, Name = "EP07", Votes = 0, ImagePath = "/images/trains/ep07.jpg" },
                new TrainRanking { TrainId = 2, Name = "EP09", Votes = 0, ImagePath = "/images/trains/ep09.jpg" },
                new TrainRanking { TrainId = 3, Name = "EU07", Votes = 0, ImagePath = "/images/trains/eu07.jpg" },
                new TrainRanking { TrainId = 1, Name = "NEWAG", Votes = 0, ImagePath = "/images/trains/newag.jpg" },
                new TrainRanking { TrainId = 2, Name = "RAIL", Votes = 0, ImagePath = "/images/trains/rail.jpg" },
                new TrainRanking { TrainId = 3, Name = "VECTRON", Votes = 0, ImagePath = "/images/trains/vectron.jpg" }
            };
        }

        public List<TrainRanking> GetRankings() => _rankings;

        public void Vote(int trainId)
        {
            var train = _rankings.FirstOrDefault(t => t.TrainId == trainId);
            if (train != null)
            {
                train.Votes++;
                Notify();
            }
        }

        public void Attach(IObserver observer) => _observers.Add(observer);

        public void Detach(IObserver observer) => _observers.Remove(observer);

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}

