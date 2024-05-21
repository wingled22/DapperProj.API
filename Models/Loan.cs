namespace Proj.API.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public int? ClientId { get; set; }
        public string Type { get; set; } = null!;
        public double Capital { get; set; }
        public double Interest { get; set; }
        public double InterestedAmount { get; set; }
        public double LoanReceivable { get; set; }
        public double TotalPayable { get; set; }
        public int NoPayment { get; set; }
        public string Status { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public double DeductCbu { get; set; }
        public double DeductInsurance { get; set; }
        public double OtherFee { get; set; }
        public double TotalPenalty { get; set; }
        public DateTime DateCreate { get; set; }
    }
    
}