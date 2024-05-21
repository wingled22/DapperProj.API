using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.API.Models
{
    public class Contribution
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCreated { get; set; }
    }

}