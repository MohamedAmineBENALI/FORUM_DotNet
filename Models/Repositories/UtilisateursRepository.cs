using System.Data.SqlClient;
namespace Forum_Dot_Net.Models.Repositories
{
    public class UtilisateursRepository : IRepository<Utilisateurs>
    {
        public IList<Utilisateurs>? Utilisateurs { get; set; }







        public void Ajouter(Utilisateurs element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT MAX(id) as Maxx FROM Utilisateurs", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    element.id = int.Parse(reader["Maxx"].ToString()) + 1;
                }
                reader.Close();
                //insertion d'une nouvelle famille
                string commandText = "insert into Utilisateurs(id,nom) values (" +
                     element.id + ",'" + element.nom + "')";
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }

        public IList<Utilisateurs> Lister()
        {
            Utilisateurs = new List<Utilisateurs>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT id,nom FROM Utilisateurs", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Utilisateurs()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        nom = reader["nom"].ToString()
                    };
                    Utilisateurs.Add(u);
                }


            }
            return Utilisateurs;

        }











        public Utilisateurs ListerSelonId(int id)
        {
            throw new NotImplementedException();
        }

        public void Modifier(int id, Utilisateurs element)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
