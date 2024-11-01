using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace FlappyC_.Models
{
    public class User: IdentityUser
    {
        [JsonIgnore]
        public virtual List<Score> Scores { get; set; } = null!;
       

    }
}
