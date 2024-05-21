using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Proj.API.Configurations;

namespace Proj.API.Data
{
    public class DapperContext
    {
        private IDatabaseProvider _databaseProvider;
        public DapperContext(IDatabaseProvider databaseProvider)
        {   
            _databaseProvider = databaseProvider;
        }

        public IDbConnection CreateConnection() => new SqliteConnection(_databaseProvider.ConnectionString);
    }
}