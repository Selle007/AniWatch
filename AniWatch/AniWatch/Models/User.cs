using Microsoft.AspNetCore.Identity;

namespace AniWatch.Models
{
    public class User : IdentityUser
    {
        public byte[]? Photo { get; set; } 
    }
}
