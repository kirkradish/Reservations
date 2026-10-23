using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Reservations.Models;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Reservations.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly string _connectionString;

        public ContactsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Person> GetAll()
        {
            var contacts = new List<Person>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllContacts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contacts.Add(new Person
                            {
                                Id = reader.GetInt32("id"),
                                FirstName = reader["first_name"].ToString(),
                                MiddleName = reader["middle_name"].ToString(),
                                LastName = reader["last_name"].ToString()
                            });
                        }
                    }
                }
            }

            return contacts;
        }

        public void AddPerson(Person newPerson)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_AddContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@firstName", newPerson.FirstName);
                    command.Parameters.AddWithValue("@middleName", string.IsNullOrEmpty(newPerson.MiddleName) ? (object)DBNull.Value : newPerson.MiddleName);
                    command.Parameters.AddWithValue("lastName", newPerson.LastName);

                    try
                    {
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) successfully inserted");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Database error: {ex.Message}");
                    }
                }
            }
        }

        public Person GetContactById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetContactById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Person
                            {
                                Id = reader.GetInt32("id"),
                                FirstName = reader["first_name"].ToString(),
                                MiddleName = reader["middle_name"].ToString(),
                                LastName = reader["last_name"].ToString()
                            };
                        }
                    }
                }
            }

            return new Person
            {
                Id = 0,
                FirstName = "",
                MiddleName = "",
                LastName = ""
            };
        }

        public void EditContact(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_EditContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", person.Id);
                    command.Parameters.AddWithValue("@firstName", person.FirstName);
                    command.Parameters.AddWithValue("@middleName", person.MiddleName);
                    command.Parameters.AddWithValue("@lastName", person.LastName);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) successfully inserted");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Database error: {ex.Message}");
                    }
                }
            }
        }
    }
}
