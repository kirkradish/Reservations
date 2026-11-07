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

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var contacts = new List<Person>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_get_all_contacts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (var reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            contacts.Add(new Person
                            {
                                Id = await reader.IsDBNullAsync(reader.GetOrdinal("user_id"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("user_id")),
                                FirstName = await reader.IsDBNullAsync(reader.GetOrdinal("first_name"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("first_name")),
                                MiddleName = await reader.IsDBNullAsync(reader.GetOrdinal("middle_name"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("middle_name")),
                                LastName = await reader.IsDBNullAsync(reader.GetOrdinal("last_name"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("last_name")),
                                Email = await reader.IsDBNullAsync(reader.GetOrdinal("email"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("email")),
                                PhoneNumber = await reader.IsDBNullAsync(reader.GetOrdinal("phone_number"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("phone_number"))
                            });
                        }
                    }
                }
            }

            return contacts;
        }

        public async Task AddPersonAsync(Person newPerson)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_add_contact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@firstName", newPerson.FirstName);
                    command.Parameters.AddWithValue("@middleName", string.IsNullOrEmpty(newPerson.MiddleName) ? (object)DBNull.Value : newPerson.MiddleName);
                    command.Parameters.AddWithValue("@lastName", newPerson.LastName);
                    command.Parameters.AddWithValue("@email", newPerson.Email);
                    command.Parameters.AddWithValue("@phoneNumber", newPerson.PhoneNumber);

                    try
                    {
                        await connection.OpenAsync();

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"{rowsAffected} row(s) successfully inserted");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Database error: {ex.Message}");
                    }
                }
            }
        }

        public async Task<Person> GetContactByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_get_contact_by_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", id);

                    await connection.OpenAsync();

                    using (var reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            return new Person
                            {
                                Id = await reader.IsDBNullAsync(reader.GetOrdinal("user_id"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("user_id")),
                                FirstName = await reader.IsDBNullAsync(reader.GetOrdinal("first_name"))
                                    ? null 
                                    : reader.GetString(reader.GetOrdinal("first_name")),
                                MiddleName = await reader.IsDBNullAsync(reader.GetOrdinal("middle_name"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("middle_name")),
                                LastName = await reader.IsDBNullAsync(reader.GetOrdinal("last_name"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("last_name")),
                                Email = await reader.IsDBNullAsync(reader.GetOrdinal("email"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("email")),
                                PhoneNumber = await reader.IsDBNullAsync(reader.GetOrdinal("phone_number"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("phone_number"))
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
                LastName = "",
                Email = "",
                PhoneNumber = ""
            };
        }

        public async Task EditContactAsync(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_edit_contact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", person.Id);
                    command.Parameters.AddWithValue("@firstName", person.FirstName);
                    command.Parameters.AddWithValue("@middleName", person.MiddleName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@lastName", person.LastName);
                    command.Parameters.AddWithValue("@email", person.Email);
                    command.Parameters.AddWithValue("@phoneNumber", person.PhoneNumber);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"{rowsAffected} row(s) successfully inserted");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Database error: {ex.Message}");
                    }
                }
            }
        }

        public async Task DeleteContactAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_soft_delete_contact", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@id", id);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
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
