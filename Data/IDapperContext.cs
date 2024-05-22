using System.Data;

namespace Proj.API.Data
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}