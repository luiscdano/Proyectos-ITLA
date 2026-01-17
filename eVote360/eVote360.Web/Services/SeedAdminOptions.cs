namespace eVote360.Web.Services;

public sealed class SeedAdminOptions
{
    public const string SectionName = "SeedAdmin";

    // Email del Admin que se garantiza en cada arranque
    public string Email { get; set; } = string.Empty;

    // Password solo se usa si el usuario NO existe y hay que crearlo
    public string Password { get; set; } = string.Empty;

    public string FullName { get; set; } = "Administrador";
}