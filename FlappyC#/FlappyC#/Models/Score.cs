namespace FlappyC_.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int LeScore { get; set; }
        public double Temps { get; set; }
        public DateTime Date { get; set; }
        public bool Visibilité { get; set; }
        
        public string Pseudo { get; set; }

        public User? User { get; set; }

      
    }
}
