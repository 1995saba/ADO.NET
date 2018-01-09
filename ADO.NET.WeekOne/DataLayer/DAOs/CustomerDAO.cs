using DataLayer.DTOs;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAOs
{
    public class CustomerDAO : IDAO<CustomerDTO>
    {
        private SqlConnection sqlConnection = null;
        public void Create(CustomerDTO t)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseInsertQuery = @"INSERT INTO [NORTHWND].[dbo].[Region] " +
                                     "(CustomerID, CompanyName, ContactName, ContactTitle, " +
                                     "Address, City, Region, PostalCode, Country, Phone, Fax) " +
                                     "VALUES ('{0}', '{1}','{2}','{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                    string realInsertQuery = String.Format(baseInsertQuery,
                        t.RegionId.ToString(),
                        t.RegionDescription);

                    sqlCommand.CommandText = realInsertQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    int result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(result);
                }
                sqlConnection.Close();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CustomerDTO Read(int id)
        {
            CustomerDTO regionDTOToReturn = null;
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseSelectQuery = @"SELECT * FROM [NORTHWND].[dbo].[Region] " +
                                     "WHERE [RegionID] = {0}";
                    string realSelectQuery = String.Format(baseSelectQuery, id.ToString());

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        regionDTOToReturn = new CustomerDTO()
                        {
                            RegionId = Int32.Parse(reader["RegionID"].ToString()),
                            RegionDescription = reader["RegionDescription"].ToString()
                        };
                    }
                }
                sqlConnection.Close();
            }
            return regionDTOToReturn;
        }

        public ICollection<CustomerDTO> Read()
        {
            List<CustomerDTO> regionDTOsToReturn = new List<CustomerDTO>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string realSelectQuery = @"SELECT * FROM [NORTHWND].[dbo].[Region]";

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            regionDTOsToReturn.Add(new CustomerDTO()
                            {
                                RegionId = Int32.Parse(reader["RegionID"].ToString()),
                                RegionDescription = reader["RegionDescription"].ToString()
                            });
                        }
                    }
                }
                sqlConnection.Close();
            }
            return regionDTOsToReturn;
        }

        public void Update(CustomerDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
