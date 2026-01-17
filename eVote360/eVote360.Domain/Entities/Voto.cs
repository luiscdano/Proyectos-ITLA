namespace eVote360.Domain.Entities
{
    public class Voto
    {
        public int Id { get; set; }

        public int EleccionId { get; set; }
        public int PuestoElectivoId { get; set; }
        public int CandidaturaId { get; set; }

        public DateTime FechaHora { get; set; }
        public string? TokenVoto { get; set; }

        public Eleccion? Eleccion { get; set; }
        public PuestoElectivo? PuestoElectivo { get; set; }
        public Candidatura? Candidatura { get; set; }
    }
}