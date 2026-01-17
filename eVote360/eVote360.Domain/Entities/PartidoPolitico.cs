namespace eVote360.Domain.Entities
{
    public class PartidoPolitico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? LogoPath { get; set; }
        public bool IsActive { get; set; } = true;
    }
}