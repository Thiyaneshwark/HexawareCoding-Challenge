using LoanManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem.Data
{
    public class LoanManagementContext : DbContext
    {
        public LoanManagementContext(DbContextOptions<LoanManagementContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<HomeLoan> HomeLoans { get; set; }
        public DbSet<CarLoan> CarLoans { get; set; }
    }
}
