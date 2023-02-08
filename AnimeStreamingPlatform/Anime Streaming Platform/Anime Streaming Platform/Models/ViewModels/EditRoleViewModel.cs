using System.ComponentModel.DataAnnotations;

namespace Anime_Streaming_Platform.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        [Required]
        public string? Id { get; set; } 
        [Required]
        public string? RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
