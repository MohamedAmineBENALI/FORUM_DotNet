using Forum_Dot_Net.Models;
using Microsoft.AspNetCore.Mvc;
using Forum_Dot_Net.Models.Repositories;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum_Dot_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        public IList<Utilisateurs>? Utilisateurs { get; set; }
        public IList<Forums>? Forums { get; set; }

        public IList<Themes>? Themes { get; set; }
        public IList<Postes>? Postes { get; set; }
        public IList<Commentaires>? Commentaires { get; set; }
        public IList<AddCmnt>? AddCmnt { get; set; }




        [HttpGet("/Login/{email}/{mdp}")]
        public int Login(string email,string mdp)
        {

            Utilisateurs = new List<Utilisateurs>();
            int _id=-1;
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(id,-1) as id FROM Utilisateurs where email='" + email+"' and mdp='"+mdp+"'", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    _id = Convert.ToInt32(reader["id"]);

                }


            }
            return _id;
        }





        // GET: api/<UtilisateurController>
        [HttpGet("/getAllUsers")]
        public IList<Utilisateurs> GetAllUtilisateurs()
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


        // GET api/<UtilisateurController>/5
        [HttpGet("/userByID/{id}")]
        public IList<Utilisateurs> GetUtilisateursId(int id)
        {

            Utilisateurs = new List<Utilisateurs>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT id,nom FROM Utilisateurs where id=" + id, conn);
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




        // POST api/<UtilisateurController>
        [HttpPost("/addUser")]
        public int AjouterUtilisateurs(Utilisateurs element)
        {
            int _id = -1;
            var connectionString1 = Global.cc;
            using (SqlConnection conn1 = new SqlConnection(connectionString1))
            {
                conn1.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd1 = new SqlCommand("SELECT ISNULL(id,-1) FROM Utilisateurs where email='" + element.email +  "'", conn1);
                SqlDataReader reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    _id = reader1.GetInt32(0);
                }


            }
            if (_id != -1)
            {
                return 0;

            }
            else
            {
                var connectionString = Global.cc;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //insertion d'une nouvelle famille
                    string commandText = "insert into Utilisateurs(id,nom,prenom,email,mdp,role,actif) values (ISNULL((SELECT MAX(id) + 1 FROM Utilisateurs), 1),'" +
                          element.nom + "','" + element.prenom + "','" + element.email + "','" + element.mdp + "',1,0)";
                    SqlCommand cmdi = new SqlCommand(commandText, conn);
                    cmdi.ExecuteNonQuery();

                }
                return 1;
            }




           
        }


      


        // PUT api/<UtilisateurController>/5
        [HttpPut("/modifierUSER/{id}")]
        public void ModifierUtilisateurs(int id, Utilisateurs element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "update Utilisateurs set nom='" + element.nom + "',prenom='"+ element.prenom + "',email='"+element.email+"',mdp='" + element.mdp + "',actif=" + element.actif+ "where id=" +
                    id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }

        // DELETE api/<UtilisateurController>/5
        [HttpDelete("/supprimerUser/{id}")]
        public void Supprimer(int id)
        {

            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "delete from Utilisateurs where id=" + id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }




        // API FORMUS---------------------------------------------

        [HttpGet("/getAllForums")]
        public IList<Forums> GetAllForums()
        {

            Forums = new List<Forums>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT id,titre,cdate,description FROM Forums ", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Forums()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        titre = reader["titre"].ToString(),
                        cdate = reader["cdate"].ToString(),
                        description = reader["description"].ToString()



                    };
                    Forums.Add(u);
                }


            }
            return Forums;
        }






        [HttpGet("/ForumsByID/{id}")]
        public IList<Forums> ForumsByID(int id)
        {

            Forums = new List<Forums>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT id,titre,cdate,description FROM Forums where id=" + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Forums()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        titre = reader["titre"].ToString(),
                        cdate = reader["cdate"].ToString(),
                        description = reader["description"].ToString()

                    };
                    Forums.Add(u);
                }


            }
            return Forums;
        }



        // POST api/<UtilisateurController>
        [HttpGet("/addFormus/{element}")]
        public void AddFormus(String element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //insertion d'une nouvelle famille
                string commandText = "insert into Forums(id,titre,cdate) values (ISNULL((SELECT MAX(id) + 1 FROM Forums), 1),'" +
                      element + "',SYSDATETIME( ))";
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }


        [HttpPost("/addFormus")]
        public void AddFormus2(Forums element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //insertion d'une nouvelle famille
                string commandText = "insert into Forums(id,titre,cdate,description) values (ISNULL((SELECT MAX(id) + 1 FROM Forums), 1),'" +
                      element.titre + "',SYSDATETIME( ),'"+element.description+"')";
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }




        [HttpGet("/modifierForums/{id}/{element}")]
        public void ModifierForums(int id, String element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "update Forums set titre='" + element+ "' where id=" +
                    id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }


        // DELETE api/<UtilisateurController>/5
        [HttpDelete("/supprimerForms/{id}")]
        public void SupprimerForms(int id)
        {

            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "delete from Forums where id=" + id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }


        // API Themes---------------------------------------------



        [HttpGet("/getAllThemes/{id_Forums}")]
        public IList<Themes> GetAllThemes(int id_Forums)
        {

            Themes = new List<Themes>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT id,titre FROM Themes where id_forums=" + id_Forums, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Themes()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        titre = reader["titre"].ToString()


                    };
                    Themes.Add(u);
                }


            }
            return Themes;
        }






        [HttpGet("/ThemesByID/{id}")]
        public IList<Themes> ThemesByID(int id)
        {

            Themes = new List<Themes>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT id,titre FROM Themes where id=" + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Themes()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        titre = reader["titre"].ToString()
                    };
                    Themes.Add(u);
                }


            }
            return Themes;
        }



        // POST api/<UtilisateurController>
        [HttpPost("/addThemes")]
        public void AddThemes(Themes element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //insertion d'une nouvelle famille
                string commandText = "insert into Themes(id,titre,id_forums) values (ISNULL((SELECT MAX(id) + 1 FROM Themes), 1),'" +
                      element.titre + "',"+ element.id_forums+")";
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }


        [HttpPut("/modifierThemes/{id}")]
        public void modifierThemes(int id, String element)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "update Themes set titre='" + element + "' where id=" +
                    id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }


        // DELETE api/<UtilisateurController>/5
        [HttpDelete("/supprimerThemes/{id}")]
        public void SupprimerThemes(int id)
        {

            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "delete from Themes where id=" + id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }







        // API Postes---------------------------------------------
        [HttpGet("/getAllPostes/{id_Themes}")]
        public IList<Postes> GetAllPostes(int id_Themes)
        {

            Postes = new List<Postes>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT a.id as id,a.post as post ,a.cdate as cdate,a.rate as rate,(select count(*) from commentaires c where c.id_poste = a.id ) as nb_comments ,a.id_personnes as id_personnes,b.nom as nom,b.prenom as prenom  FROM Postes a,Utilisateurs b where a.id_personnes=b.id and a.id_themes=" + id_Themes, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Postes()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        post = reader["post"].ToString(),
                        cdate = reader["cdate"].ToString(),
                        rate = Convert.ToInt32(reader["rate"]),
                        nb_comments = Convert.ToInt32(reader["nb_comments"]),
                        id_personnes = Convert.ToInt32(reader["id_personnes"]),
                        nom = reader["nom"].ToString(),
                        prenom = reader["prenom"].ToString(),
                    };
                    Postes.Add(u);
                }


            }
            return Postes;
        }






        [HttpGet("/PostesByID/{id}")]
        public IList<Postes> PostesByID(int id)
        {

            Postes = new List<Postes>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT a.id as id,a.post as post ,a.cdate as cdate,a.rate as rate,ISNULL(a.nb_comments,0) as nb_comments ,a.id_personnes as id_personnes,b.nom as nom,b.prenom as prenom  FROM Postes a,Utilisateurs b where a.id_personnes=b.id and a.id=" + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Postes()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        post = reader["post"].ToString(),
                        cdate = reader["cdate"].ToString(),
                        rate = Convert.ToInt32(reader["rate"]),
                        nb_comments = Convert.ToInt32(reader["nb_comments"]),
                        id_personnes = Convert.ToInt32(reader["id_personnes"]),
                        nom = reader["nom"].ToString(),
                        prenom = reader["prenom"].ToString(),
                    };
                    Postes.Add(u);
                }


            }
            return Postes;
        }

     

                // POST api/<UtilisateurController>
                [HttpPost("/AddPost")]
                public void AddPost(Postes element)
                {
                    var connectionString = Global.cc;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        //insertion d'une nouvelle famille
                        string commandText = "insert into Postes(id,cdate,rate,post,id_themes,id_personnes) values" +
                    " (ISNULL((SELECT MAX(id) + 1 FROM Postes), 1),SYSDATETIME( ),0,'" +
                              element.post + "'," + element.id_themes +","+ element.id_personnes + ")";
                        SqlCommand cmdi = new SqlCommand(commandText, conn);
                        cmdi.ExecuteNonQuery();

                    }
                }
     

                     [HttpPut("/modifierPost/{id}")]
                     public int modifierPost(int id, int rate)
                     {
                         var connectionString = Global.cc;
                         using (SqlConnection conn = new SqlConnection(connectionString))
                         {
                             conn.Open();
                             //mettre à jour une famille
                             string commandText = "update postes set rate=CAST(ROUND((rates+" + rate + ")/5, 0) AS INT) where id=" +
                                 id;
                             SqlCommand cmdi = new SqlCommand(commandText, conn);
                             cmdi.ExecuteNonQuery();

                         }
            return 0;
                     }


        [HttpGet("/modifierRate/{id}/{rate}")]
        public void modifierRate(int id, int rate)
        {
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //mettre à jour une famille
                string commandText = "update postes set rate=" + rate + " where id=" +
                    id;
                SqlCommand cmdi = new SqlCommand(commandText, conn);
                cmdi.ExecuteNonQuery();

            }
        }



        // DELETE api/<UtilisateurController>/5
        [HttpDelete("/supprimerPostes/{id}")]
                          public void supprimerPostes(int id)
                          {

                              var connectionString = Global.cc;
                              using (SqlConnection conn = new SqlConnection(connectionString))
                              {
                                  conn.Open();
                                  //mettre à jour une famille
                                  string commandText = "delete from Postes where id=" + id;
                                  SqlCommand cmdi = new SqlCommand(commandText, conn);
                                  cmdi.ExecuteNonQuery();

                              }
                          }



        // API Commentaires---------------------------------------------



        [HttpGet("/getAllCommentaires/{id_Poste}")]
        public IList<Commentaires> getAllCommentaires(int id_Poste)
        {

            Commentaires = new List<Commentaires>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT a.id as id,a.Commentaire as Commentaire ,a.cdate as cdate,a.rate as rate," +
                    " a.id_personnes as id_personnes,b.nom as nom,b.prenom as prenom " +
                    " FROM Commentaires a,Utilisateurs b where a.id_personnes=b.id and a.id_Poste=" + id_Poste, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Commentaires()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        Commentaire = reader["Commentaire"].ToString(),
                        cdate = reader["cdate"].ToString(),
                        rate = Convert.ToInt32(reader["rate"]),
                        id_personnes = Convert.ToInt32(reader["id_personnes"]),
                        nom = reader["nom"].ToString(),
                        prenom = reader["prenom"].ToString(),
                    };
                    Commentaires.Add(u);
                }


            }
            return Commentaires;
        }






        [HttpGet("/CommentairesByID/{id}")]
        public IList<Commentaires> CommentairesByID(int id)
        {
            Commentaires = new List<Commentaires>();
            var connectionString = Global.cc;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //récupérer le max de id dans la table Famille
                SqlCommand cmd = new SqlCommand("SELECT a.id as id,a.Commentaire as Commentaire ,a.cdate as cdate,a.rate as rate," +
                    " ,a.id_personnes as id_personnes,b.nom as nom,b.prenom as prenom " +
                    " FROM Commentaires a,Utilisateurs b where a.id_personnes=b.id and a.id_Poste=" + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new Commentaires()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        Commentaire = reader["Commentaire"].ToString(),
                        cdate = reader["cdate"].ToString(),
                        rate = Convert.ToInt32(reader["rate"]),
                        id_personnes = Convert.ToInt32(reader["id_personnes"]),
                        nom = reader["nom"].ToString(),
                        prenom = reader["prenom"].ToString(),
                    };
                    Commentaires.Add(u);
                }


            }
            return Commentaires;
        }
    

                // POST api/<UtilisateurController>
                [HttpPost("/AddCommentaires")]
                public void AddCommentaires(AddCmnt element)
                {
                    var connectionString = Global.cc;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        //insertion d'une nouvelle famille
                        string commandText = "insert into Commentaires(id,cdate,rate,Commentaire,id_Poste,id_personnes) values" +
                    " (ISNULL((SELECT MAX(id) + 1 FROM Commentaires), 1),SYSDATETIME( ),0,'" +
                              element.Commentaire + "'," + element.id_Poste + "," + element.id_personnes + ")";
                        SqlCommand cmdi = new SqlCommand(commandText, conn);
                        cmdi.ExecuteNonQuery();

                    }
                }
        /*

                    [HttpPut("/modifierPost/{id}")]
                    public void modifierPost(int id, int rate)
                    {
                        var connectionString = Global.cc;
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            //mettre à jour une famille
                            string commandText = "update postes set rate=" + rate + " where id=" +
                                id;
                            SqlCommand cmdi = new SqlCommand(commandText, conn);
                            cmdi.ExecuteNonQuery();

                        }
                    }


                    // DELETE api/<UtilisateurController>/5
                    [HttpDelete("/supprimerPostes/{id}")]
                    public void supprimerPostes(int id)
                    {

                        var connectionString = Global.cc;
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            //mettre à jour une famille
                            string commandText = "delete from Postes where id=" + id;
                            SqlCommand cmdi = new SqlCommand(commandText, conn);
                            cmdi.ExecuteNonQuery();

                        }
                    }




            */























    }
}
