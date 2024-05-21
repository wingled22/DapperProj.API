namespace Proj.API.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public Guid LoanId { get; set; }
        public double Collectables { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = null!;
    }

}