using System.ComponentModel.DataAnnotations;

namespace PassengerPortal.Shared.Models;

public class TrainRanking
{

    public int TrainId { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public int Votes { get; set; }
}
