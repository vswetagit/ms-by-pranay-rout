using Microsoft.AspNetCore.Identity;

namespace UserService.Infra.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}
