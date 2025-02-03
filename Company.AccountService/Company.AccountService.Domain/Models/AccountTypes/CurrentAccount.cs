namespace Company.AccountService.Domain.Models.AccountTypes
{
    public class CurrentAccount : Account
    {
        public decimal OverdraftLimit { get; set; } = 0;
    }
}
