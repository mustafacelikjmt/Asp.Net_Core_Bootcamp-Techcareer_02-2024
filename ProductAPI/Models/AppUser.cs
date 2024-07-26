using Microsoft.AspNetCore.Identity;

namespace ProductAPI.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; } = null!;
        public DateTime DataAdded { get; set; }
    }
}