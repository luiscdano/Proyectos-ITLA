namespace eVote360.Domain.Entities
{
    public class Candidatura
    {
        public int Id { get; set; }

        public int EleccionId { get; set; }
        public int PuestoElectivoId { get; set; }
        public int PartidoPoliticoId { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;
        public string? NombreBoleta { get; set; }
        public string? FotoPath { get; set; }
        public bool IsActive { get; set; }

        public Eleccion? Eleccion { get; set; }
        public PuestoElectivo? PuestoElectivo { get; set; }
        public PartidoPolitico? PartidoPolitico { get; set; }

        public ICollection<Voto> Votos { get; set; } = new List<Voto>();
    }
}