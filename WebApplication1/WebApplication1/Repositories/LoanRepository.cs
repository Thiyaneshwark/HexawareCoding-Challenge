using LoanManagementSystem.Data;
using LoanManagementSystem.Entities;
using LoanManagementSystem.Exceptions;
using LoanManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanManagementContext _context;

        public LoanRepository(LoanManagementContext context)
        {
            _context = context;
        }

        public async Task ApplyLoan(Loan loan)
        {
            loan.LoanStatus = "Pending";
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> CalculateInterest(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");

            return (loan.PrincipalAmount * (decimal)loan.InterestRate * loan.LoanTerm) / 12;
        }

        public async Task<string> LoanStatus(int loanId)
        {
            var loan = await _context.Loans.Include(l => l.Customer).FirstOrDefaultAsync(l => l.LoanId == loanId);
            if (loan == null)
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");

            loan.LoanStatus = loan.Customer.CreditScore > 650 ? "Approved" : "Rejected";
            await _context.SaveChangesAsync();
            return loan.LoanStatus;
        }

        public async Task<decimal> CalculateEMI(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");

            decimal monthlyRate = ((decimal)loan.InterestRate / 12) / 100;
            int n = loan.LoanTerm;
            decimal emi = loan.PrincipalAmount * monthlyRate * (decimal)Math.Pow(1 + (double)monthlyRate, n) / ((decimal)Math.Pow(1 + (double)monthlyRate, n) - 1);
            return emi;
        }

        public async Task LoanRepayment(int loanId, decimal amount)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");

            decimal emi = await CalculateEMI(loanId);
            if (amount < emi)
                throw new InvalidLoanException("Amount is less than EMI.");

            int noOfEMIs = (int)(amount / emi);
            // Repayment logic here...
        }

        public async Task<List<Loan>> GetAllLoans()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task<Loan> GetLoanById(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                throw new InvalidLoanException($"Loan with ID {loanId} not found.");
            return loan;
        }
    }
}
