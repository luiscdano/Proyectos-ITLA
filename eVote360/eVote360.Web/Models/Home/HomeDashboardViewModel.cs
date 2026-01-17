namespace eVote360.Web.Models.Home
{
    public class HomeDashboardViewModel
    {
        public int TotalVotos { get; set; }
        public int TotalPartidos { get; set; }
        public int TotalCandidaturas { get; set; }

        public List<PartyCardViewModel> Partidos { get; set; } = new();
    }
}