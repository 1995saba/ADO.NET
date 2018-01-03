using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionStringOne"].ToString();

            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder(defaultConnectionString);
            sqlConnectionString.UserID = "sa";
            sqlConnectionString.Password = "Shag1115";
            sqlConnectionString.DataSource = "SQL";
            sqlConnectionString.InitialCatalog = "NORTHWND";
            sqlConnectionString.PersistSecurityInfo = false;

            string connectionString = sqlConnectionString.ToString();

            //string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionStringOne"].ToString();

            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = @"SELECT COUNT(name) AS NumberOfRecords FROM sys.tables";
                sqlCommand.CommandType = CommandType.Text;

                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if(sqlDataReader.Read())
                {
                    Console.WriteLine(sqlDataReader["NumberOfRecords"]);
                }
                sqlConnection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
