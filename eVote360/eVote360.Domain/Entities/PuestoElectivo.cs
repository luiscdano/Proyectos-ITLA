namespace eVote360.Domain.Entities
{
    public class PuestoElectivo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool IsActive { get; set; } = true;
    }
}