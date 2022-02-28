using System;
using System.Data;
using System.Data.SqlClient;

namespace Demo_Secu_InjectionSQL_ADO
{
   class Program
   {
      // NB: Dans un projet "réel" => Dans le fichier de config ♥
      private const string CONNECTION_STRING = "Data Source=TFNSDOTDE0500A;Initial Catalog=DemoFaille;Integrated Security=True";

      static void Main(string[] args)
      {
         // En .Net Core, il est necessaire s'installer le nugget package :
         //  "System.Data.SqlClient"

         #region Intro à ADO
         ViewAllMember();
         ViewCountMember();
         #endregion

      }

      private static void ViewCountMember()
      {
         // Utilisation du "using" pour libérer les resources de connection
         // via l'appel de la méthode "dispose" à la fin du bloc.
         using(SqlConnection connection = new SqlConnection())
         {
            connection.ConnectionString = CONNECTION_STRING;
            
            using(SqlCommand command = connection.CreateCommand())
            {
               // Définition de la commande SQL
               command.CommandType = CommandType.Text;
               command.CommandText = "SELECT COUNT(*) FROM Member";

               // Ouverture de la connection
               connection.Open();

               int count = Convert.ToInt32(command.ExecuteScalar());
               Console.WriteLine($"Nombre de membre : {count}");
            }
         }
      }

      private static void ViewAllMember()
      {
         // SQLConnection: Connection avec le DB + Interaction
         SqlConnection connection = new SqlConnection();
         connection.ConnectionString = CONNECTION_STRING;

         // SQLCommand: Représente une requete SQL
         SqlCommand command = connection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "SELECT IdMember, Pseudo FROM Member";

         // Ouverture de la connection vers la DB
         connection.Open();

         // Execution de la commande SQL
         // - ExecuteReader: Lecture de plusieurs "row"
         // - ExecuteScalar: Permet d'obtenir une valeur (SELECT COUNT($) ...)
         // - ExecuteNonQuery: Renvoi le nombre de ligne affecté (INSERT / UPDATE / DELETE)
         SqlDataReader reader = command.ExecuteReader();

         // Parcourir les resultats du reader
         while(reader.Read())
         {
            int idMember = (int)reader["IdMember"];
            //  Alternative possible → Convert.ToInt32(reader["IdMember"]);
            string pseudo = reader["Pseudo"].ToString();

            Console.WriteLine($"Id: {idMember} / Pseudo: {pseudo}");
         }

         // Fermeture du reader
         reader.Close();

         // Fermeture de la connection
         connection.Close();
      }


   }
}
