namespace eVote360.Web.Models.Home
{
    public class PartyCardViewModel
    {
        public int PartidoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;

        public string? Presidente { get; set; }
        public string? Vicepresidente { get; set; }

        public int Votos { get; set; }
        public double Porcentaje { get; set; }  // 0..100

        public string? LogoPath { get; set; }
    }
}