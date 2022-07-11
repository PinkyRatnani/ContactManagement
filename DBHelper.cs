using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TopoChallenge.Interfaces;
using TopoChallenge.Models;

namespace TopoChallenge.Helpers
{
    public class DBHelper : IDBHelper
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public DBHelper(string connectionString)
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.ConnectionString = connectionString == null ? throw(new ArgumentNullException()) : connectionString;
            this.sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public async Task<List<ContactDetails>> GetContactDetails()
        {
            var contactDetails = new List<ContactDetails>();

            using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    var sql = "SELECT * from dbo.ContactsDetails";

                    using (SqlCommand sqlcommand = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader =  await sqlcommand.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                contactDetails.Add(new ContactDetails
                                {
                                    Id = new Guid(reader["Id"].ToString()),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    EmailId = reader["EmailId"].ToString(),
                                    Notes = reader["Notes"].ToString(),
                                    OrgName = reader["OrgName"].ToString(),
                                    Tags = reader["Tags"].ToString(),
                                    WebsiteUrl = reader["WebsiteUrl"].ToString(),
                                    PhoneNo = Convert.ToInt32( reader["PhoneNo"].ToString())
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return contactDetails;
        }

        public async Task<ContactDetails> SaveContactDetails(ContactDetails contactDetails)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    string query = "Insert Into dbo.ContactsDetails (Id, FirstName, Lastname, Address, PhoneNo, EmailId, Notes,Category, OrgName, WebsiteUrl, Tags) " +
                   "VALUES (@Id, @FirstName, @LastName, @Address, @PhoneNo, @EmailId, @Notes, @Category, @OrgName, @WebsiteUrl, @Tags) ";

                    using (SqlCommand sqlcommand = new SqlCommand(query, connection))
                    {
                        sqlcommand.CommandType = System.Data.CommandType.Text;
                        sqlcommand.Parameters.AddWithValue("@Id", contactDetails.Id);
                        sqlcommand.Parameters.AddWithValue("@FirstName", contactDetails.FirstName);
                        sqlcommand.Parameters.AddWithValue("@LastName", contactDetails.LastName);
                        sqlcommand.Parameters.AddWithValue("@Address", contactDetails.Address);
                        sqlcommand.Parameters.AddWithValue("@PhoneNo", contactDetails.PhoneNo);
                        sqlcommand.Parameters.AddWithValue("@EmailId", contactDetails.EmailId);
                        sqlcommand.Parameters.AddWithValue("@Notes", contactDetails.Notes);
                        sqlcommand.Parameters.AddWithValue("@Category", contactDetails.Category);
                        sqlcommand.Parameters.AddWithValue("@OrgName", contactDetails.OrgName);
                        sqlcommand.Parameters.AddWithValue("@WebsiteUrl", contactDetails.WebsiteUrl);
                        sqlcommand.Parameters.AddWithValue("@Tags", contactDetails.Tags);

                        connection.Open();
                        await sqlcommand.ExecuteNonQueryAsync();
                        return contactDetails;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public async Task<List<ContactDetails>> SearchContactDetails(string searchString)
        {
            var contactDetails = new List<ContactDetails>();

            using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    var command = string.Format("select * from dbo.ContactsDetails where FirstName LIKE'%{0}%' OR" +
                           " LastName LIKE'%{0}%' OR EmailId LIKE'%{0}%' OR " +
                           " Category LIKE'%{0}%' OR PhoneNo LIKE'%{0}%' OR WebsiteUrl LIKE'%{0}%' OR Notes LIKE'%{0}%'", searchString);

                    using (SqlCommand sqlcommand = new SqlCommand(command, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = await sqlcommand.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                contactDetails.Add(new ContactDetails
                                {
                                    Id = new Guid(reader["Id"].ToString()),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    EmailId = reader["EmailId"].ToString(),
                                    Notes = reader["Notes"].ToString(),
                                    OrgName = reader["OrgName"].ToString(),
                                    Tags = reader["Tags"].ToString(),
                                    WebsiteUrl = reader["WebsiteUrl"].ToString(),
                                    PhoneNo = Convert.ToInt32(reader["PhoneNo"].ToString())
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return contactDetails;
        }
    }
}
