using Proj.API.Models;

namespace Proj.API.DTO
{
    public class ClientContributionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double TotalContribution { get; set; }
        public List<Contribution>? Contributions { get; set; } = null;
    }
}
