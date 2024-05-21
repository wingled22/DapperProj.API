using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.API.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; }= string.Empty;
        public string Birthdate { get; set; }= string.Empty;
        public string Address { get; set; }= string.Empty;
        public string EmailAddress { get; set; }= string.Empty;
        public string ContactNumber { get; set; }= string.Empty;
    }


}