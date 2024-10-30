using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlappyC_.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; } = null!; 
        [Range(0, Int32.MaxValue)]
        public int Upvotes { get; set; }
        [Range(0, Int32.MaxValue)]
        public int Downvotes { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
