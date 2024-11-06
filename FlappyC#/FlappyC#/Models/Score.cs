using System.Text.Json.Serialization;

namespace FlappyC_.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int LeScore { get; set; }
        public double Temps { get; set; }
        public DateTime? Date { get; set; }
        public bool Visibilite { get; set; }
        

        [JsonIgnore]

        public virtual User? User { get; set; }

      
    }
}
