using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace FlappyC_.Models
{
    public class User: IdentityUser
    {
        [JsonIgnore]
        public List<Score> Scores = null!;
       

    }
}
