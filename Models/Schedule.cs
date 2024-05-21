namespace Proj.API.Models
{
    public class Schedule
    {
        public string Id { get; set; }
        public string LoanId { get; set; }
        public double Collectables { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }

}