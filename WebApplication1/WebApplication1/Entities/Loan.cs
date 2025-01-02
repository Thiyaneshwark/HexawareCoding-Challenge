namespace LoanManagementSystem.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public decimal PrincipalAmount { get; set; }
        public double InterestRate { get; set; }
        public int LoanTerm { get; set; } // Months
        public string LoanType { get; set; }
        public string LoanStatus { get; set; } // Pending, Approved
    }
}
