using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.API.Configurations
{
    public interface IDatabaseProvider
    {
        public string ConnectionString { get; set; }
    }
}