namespace eVote360.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public bool IsActive { get; set; } = true; // Borrado lógico
}