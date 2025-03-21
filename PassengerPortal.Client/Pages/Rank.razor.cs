using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using PassengerPortal.Shared.Interfaces;
using PassengerPortal.Client.Services;
using PassengerPortal.Shared.Models;


namespace PassengerPortal.Client.Pages
{
    public partial class Rank : ComponentBase, IObserver
    {
        [Inject] private HttpClient Http { get; set; }

        private List<TrainRanking> rankings;
        private int visibleCount = 4;

        private RankingService _rankingService;

        protected override async Task OnInitializedAsync()
        {
            // Pobranie danych rankingu
            rankings = await Http.GetFromJsonAsync<List<TrainRanking>>("api/Ranking");

            // Rejestracja obserwatora
            _rankingService = new RankingService();
            _rankingService.Attach(this);
        }

        public void Update()
        {
            InvokeAsync(async () =>
            {
                rankings = await Http.GetFromJsonAsync<List<TrainRanking>>("api/Ranking");
                StateHasChanged();
            });
        }

        private async Task VoteForTrain(int trainId)
        {
            await Http.PostAsync($"api/Ranking/vote/{trainId}", null);
        }

        public void Dispose()
        {
            _rankingService?.Detach(this);
        }
    }

}