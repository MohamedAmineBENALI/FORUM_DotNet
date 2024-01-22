namespace Forum_Dot_Net.Models
{
    public class Utilisateurs
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string mdp { get; set; }
        public int role { get; set; }
        public int actif { get; set; }

    }
}
