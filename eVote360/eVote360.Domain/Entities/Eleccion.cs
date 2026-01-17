namespace eVote360.Domain.Entities
{
    public class Eleccion
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();
        public ICollection<Voto> Votos { get; set; } = new List<Voto>();
    }
}