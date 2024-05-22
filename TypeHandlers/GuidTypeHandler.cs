﻿namespace Proj.API.TypeHandlers
{
    using System;
    using Dapper;
    using System.Data;

    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }

        public override Guid Parse(object value)
        {
            return Guid.Parse(value.ToString());
        }
    }

}
