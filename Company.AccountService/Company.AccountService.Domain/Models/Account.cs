using Company.AccountService.Domain.Models.Base;

namespace Company.AccountService.Domain.Models
{
    public abstract class Account : DataBase
    {
        public required string DisplayName { get; set; }
        public required string AccountNumber { get; set; }
        public required string SortCode { get; set; }

        public decimal Balance { get; set; } = 0;
        public bool IsFrozen { get; set; } = false;

        protected Account() => Id = Guid.NewGuid();
    }
}
