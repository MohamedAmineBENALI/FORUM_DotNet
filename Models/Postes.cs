namespace Forum_Dot_Net.Models
{
    public class Postes
    {
        public int id { get; set; } 
        public string post { get; set; }
        public string cdate { get; set; }
        public int rate { get; set; }
        public int nb_comments { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }

        public int id_themes { get; set; }

        public int id_personnes { get; set; }

    }
}
