using Dapper;
using Proj.API.Data;
using Proj.API.Models;

namespace Proj.API.Repositories
{
    public class ContributionRepository
    {
        private DapperContext _context;
        public ContributionRepository(DapperContext context)
        {
            _context = context;
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
    }
}
