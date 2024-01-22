namespace Forum_Dot_Net.Models
{
    public class Commentaires
    {
        public int id { get; set; }
        public string Commentaire { get; set; }
        public string cdate { get; set; }
        public int rate { get; set; }
        public int id_personnes { get; set; }
        public int id_Poste { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }


    }
}
