namespace Company.AccountService.Domain.Models.AccountTypes
{
    public class SavingsAccount : Account
    {
        public decimal RateOfInterest { get; set; }
    }
}
