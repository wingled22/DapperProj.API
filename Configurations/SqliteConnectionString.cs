using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.API.Configurations
{
    public class SqliteConnectionString : IDatabaseProvider
    {
        public string ConnectionString { get; set; } = null!;

        public void Validate() => ArgumentException.ThrowIfNullOrWhiteSpace(nameof(ConnectionString));
        
    }
}