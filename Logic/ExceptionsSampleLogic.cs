using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperProj.API.Middleware;

namespace DapperProj.API.Logic
{
    public class ExceptionsSampleLogic
    {
         public string ReturnException(string s ){
            ArgumentNullException.ThrowIfNullOrEmpty( "param is empty");
            ArgumentNullException.ThrowIfNullOrWhiteSpace( "param is empty");
            return s;
        }
    }
}