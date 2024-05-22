using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Proj.API.Data;
using Proj.API.Models;

namespace Proj.API.Repositories
{
    public class ClientRepository
    {
        private readonly DapperContext _context;

        public ClientRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            var query = "SELECT * FROM Clients";

            using (var connection = _context.CreateConnection())
            {
                var clients = await connection.QueryAsync<Client>(query);
                return clients;
            }
        }

        public async Task<Client> GetClientById(int id)
        {
            var query = "SELECT * FROM Clients WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var client = await connection.QuerySingleOrDefaultAsync<Client>(query, new { Id = id });
                return client;
            }
        }

        public async Task<Client> CreateClient(Client client)
        {
            var query = @" INSERT INTO Clients (Name, Gender, Birthdate, Address, EmailAddress, ContactNumber) 
                        VALUES (@Name, @Gender, @Birthdate, @Address, @EmailAddress, @ContactNumber);
                        SELECT * FROM Clients WHERE id = last_insert_rowid();";

            using (var connection = _context.CreateConnection())
            {
                var clientId = await connection.QuerySingleOrDefaultAsync<Client>(query, client);
                return clientId;
            }
        }

        public async Task<Client> UpdateClient(Client client)
        {
            var query = @"UPDATE Clients 
                      SET Name = @Name, Gender = @Gender, Birthdate = @Birthdate, 
                          Address = @Address, EmailAddress = @EmailAddress, ContactNumber = @ContactNumber
                      WHERE Id = @Id;
                      SELECT * FROM Clients WHERE id = @Id;";

            using (var connection = _context.CreateConnection())
            {
                var updatedClient = await connection.QueryFirstOrDefaultAsync<Client>(query, client);
                return updatedClient;
            }
        }

        public async Task<bool> DeleteClient(int id)
        {
            var query = "DELETE FROM Clients WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }
    }
}