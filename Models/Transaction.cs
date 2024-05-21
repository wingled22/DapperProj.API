namespace Proj.API.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string AddedBy { get; set; }
        public int ClientId { get; set; }
        public string ScheduleId { get; set; }
        public string LoanId { get; set; }
    }

}