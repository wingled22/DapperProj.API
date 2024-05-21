namespace Proj.API.Models
{
    public class Loan
    {
        public string Id { get; set; }
        public int? ClientId { get; set; }
        public string Type { get; set; }
        public double Capital { get; set; }
        public double Interest { get; set; }
        public double InterestedAmount { get; set; }
        public double LoanReceivable { get; set; }
        public double TotalPayable { get; set; }
        public int NoPayment { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public double DeductCbu { get; set; }
        public double DeductInsurance { get; set; }
        public double OtherFee { get; set; }
        public double TotalPenalty { get; set; }
        public DateTime DateCreate { get; set; }
    }
    
}