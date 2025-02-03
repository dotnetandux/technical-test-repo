using Company.AccountService.Domain.Models.AccountTypes;

namespace Company.AccountService.Test.Helpers
{
    public static class ModelHelper
    {
        private static string ValidAccountId { get; set; } = "35030502";
        private static string ValidSortCode { get; set; } = "347513";

        public static CurrentAccount ValidCurrentAccount()
            => new()
            {
                AccountNumber = ValidAccountId,
                SortCode = ValidSortCode,
                DisplayName = "account test",
                OverdraftLimit = 0
            };

        public static SavingsAccount ValidSavingsAccount()
            => new()
            {
                AccountNumber = ValidAccountId,
                SortCode = ValidSortCode,
                DisplayName = "account test",
                RateOfInterest = 0
            };
    }
}
