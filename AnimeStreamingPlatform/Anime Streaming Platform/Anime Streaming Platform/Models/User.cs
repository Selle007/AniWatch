using Microsoft.AspNetCore.Identity;

namespace Anime_Streaming_Platform.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
    }
}
