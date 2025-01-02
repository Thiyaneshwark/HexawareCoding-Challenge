using LoanManagementSystem.Entities;

namespace LoanManagementSystem.Interfaces
{
    public interface ILoanRepository
    {
        Task ApplyLoan(Loan loan);
        Task<decimal> CalculateInterest(int loanId);
        Task<string> LoanStatus(int loanId);
        Task<decimal> CalculateEMI(int loanId);
        Task LoanRepayment(int loanId, decimal amount);
        Task<List<Loan>> GetAllLoans();
        Task<Loan> GetLoanById(int loanId);
    }
}
