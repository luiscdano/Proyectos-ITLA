using System.Security.Claims;

namespace eVote360.Web.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _http;

        public UserContextService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string? UserId => _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? Email => _http.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

        public bool IsAuthenticated => _http.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public bool IsAdmin => _http.HttpContext?.User?.IsInRole("Admin") ?? false;
    }
}