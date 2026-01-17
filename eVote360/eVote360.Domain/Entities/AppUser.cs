using Microsoft.AspNetCore.Identity;

namespace eVote360.Domain.Entities
{
    // Usuario de la app (Identity)
    public class AppUser : IdentityUser
    {
        // Extra opcional (si quieres mostrar nombre real en UI)
        public string? FullName { get; set; }

        // Auditoría simple
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}