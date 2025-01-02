using LoanManagementSystem.Entities;
using LoanManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _repository;

        public LoanController(ILoanRepository repository)
        {
            _repository = repository;
        }

        // Endpoint to apply a loan
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyLoan([FromBody] Loan loan)
        {
            await _repository.ApplyLoan(loan);
            return Ok("Loan applied successfully!");
        }

        // Endpoint to calculate the interest for a loan
        [HttpGet("calculate-interest/{loanId}")]
        public async Task<IActionResult> CalculateInterest(int loanId)
        {
            try
            {
                var interest = await _repository.CalculateInterest(loanId);
                return Ok(new { LoanId = loanId, Interest = interest });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Endpoint to check the status of a loan
        [HttpGet("status/{loanId}")]
        public async Task<IActionResult> LoanStatus(int loanId)
        {
            try
            {
                var status = await _repository.LoanStatus(loanId);
                return Ok(new { LoanId = loanId, Status = status });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Endpoint to calculate EMI for a loan
        [HttpGet("calculate-emi/{loanId}")]
        public async Task<IActionResult> CalculateEMI(int loanId)
        {
            try
            {
                var emi = await _repository.CalculateEMI(loanId);
                return Ok(new { LoanId = loanId, EMI = emi });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Endpoint to handle loan repayment
        [HttpPost("repayment/{loanId}")]
        public async Task<IActionResult> LoanRepayment(int loanId, [FromBody] decimal amount)
        {
            try
            {
                await _repository.LoanRepayment(loanId, amount);
                return Ok("Repayment processed successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint to get all loans
        [HttpGet("all")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _repository.GetAllLoans();
            return Ok(loans);
        }

        // Endpoint to get a loan by its ID
        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetLoanById(int loanId)
        {
            try
            {
                var loan = await _repository.GetLoanById(loanId);
                return Ok(loan);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
