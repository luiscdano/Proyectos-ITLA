using System.Security.Claims;

namespace eVote360.Web.Services
{
    public interface IUserContextService
    {
        string? UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
    }
}