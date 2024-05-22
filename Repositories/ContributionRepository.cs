using Dapper;
using Proj.API.Data;
using Proj.API.DTO;
using Proj.API.Models;

namespace Proj.API.Repositories
{
    public class ContributionRepository
    {
        private IDapperContext _context;
        public ContributionRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<Contribution> SaveContribution(Contribution contribution){
            const string query = @"Insert into Contributions (Id,Amount, ClientId, DateCreated) 
                                values (@Id, @Amount, @ClientId, @DateCreated);";
            contribution.Id = Guid.NewGuid();
            contribution.DateCreated = DateTime.Now.Date;
            using(var connection = _context.CreateConnection()){
                var createdContribution = await connection.QuerySingleOrDefaultAsync<Contribution>(query, contribution);
                return createdContribution!;
            }
        }

        public async Task<IEnumerable<Contribution>> GetContributions(int clientId)
        {
            const string query = @"select * from Contributions where ClientId = @clientId";

            using(var connection =  _context.CreateConnection())
            {
                var contributions = await connection.QueryAsync<Contribution>(query, new {clientId =clientId});
                return contributions;
            }
        } 

        public async Task<ClientContributionDTO> GetClientContributionSummary(int clientId)
        {
            const string query = @"SELECT c.Id, c.Name, COALESCE(SUM(co.Amount), 0) AS TotalContribution
                                FROM Clients c 
                                LEFT JOIN Contributions co ON c.Id = co.ClientId
                                WHERE c.Id = @clientId";

            using( var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<ClientContributionDTO>(query, new { clientId = clientId });

                if(result == null)
                    return result;

                const string contributionsQuery = @"SELECT * FROM Contributions WHERE ClientId = @clientId";
                result.Contributions = (await connection.QueryAsync<Contribution>(contributionsQuery, new { clientId = clientId })).AsList();

                return result;

            }
        }


        //TODO : tiwason sa logic later
        public async Task<IEnumerable<ClientContributionDTO>> GetAllClientContributionSummaries()
        {
            const string query = @"
                SELECT c.Id, c.Name, COALESCE(SUM(co.Amount), 0) AS TotalContribution
                FROM Clients c 
                LEFT JOIN Contributions co ON c.Id = co.ClientId
                ";

            using (var connection = _context.CreateConnection())
            {
                var clientSummaries = await connection.QueryAsync<ClientContributionDTO>(query);

                foreach (var client in clientSummaries)
                {
                    const string contributionsQuery = "SELECT * FROM Contributions WHERE ClientId = @clientId";
                    client.Contributions = (await connection.QueryAsync<Contribution>(contributionsQuery, new { clientId = client.Id })).AsList();
                }

                return clientSummaries;
            }
        }
    }
}
