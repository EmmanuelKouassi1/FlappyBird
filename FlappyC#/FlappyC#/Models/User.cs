using Microsoft.AspNetCore.Identity;

namespace FlappyC_.Models
{
    public class User: IdentityUser
    {
        public List<Score> Scores = null!;

    }
}
