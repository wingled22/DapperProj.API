namespace Proj.API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string AddedBy { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public Guid ScheduleId { get; set; }
        public Guid LoanId { get; set; }
    }

}