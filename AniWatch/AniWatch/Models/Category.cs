using System.ComponentModel.DataAnnotations;

namespace AniWatch.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDesc { get; set; }
    }
}
