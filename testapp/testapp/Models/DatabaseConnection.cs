using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace testapp.Models
{
    public class DatabaseConnection
    {
        private static string connectionString = WebConfigurationManager.ConnectionStrings["Clients"].ConnectionString;

        public static List<Client> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM Clients", connection);
            List<Client> data = new List<Client>();

            using (connection)
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Client data_element = new Client();

                    data_element.id = reader.GetInt32(0);
                    data_element.name = reader.GetString(1);
                    data_element.payment = reader.GetDouble(2);
                    data_element.creation_date = reader.GetDateTime(3);

                    data.Add(data_element);
                }
            }

            return data;
        }

        public static int Add(Client client)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(@"INSERT INTO Clients(name, payment, creation_date)  
                                                  VALUES ('" + client.name + "', " + client.payment + ", '" + client.creation_date + "')", connection);
            int add_id = 0;

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();

                command.CommandText = "SELECT MAX(ID) FROM Clients";
                add_id = Convert.ToInt32(command.ExecuteScalar());
            }

            return add_id;
        }

        public static void Update(Client client)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(@"UPDATE Clients SET name = '" + client.name + "', payment = " + client.payment + 
                                                 " WHERE id = " + client.id, connection);

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void Remove(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("DELETE FROM Clients WHERE id = " + id, connection);

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}