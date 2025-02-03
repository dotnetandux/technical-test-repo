using Company.AccountService.Test.Helpers;

namespace Company.AccountService.Domain.Tests.Models
{
    public class AccountTests
    {
        [Fact]
        public void Account_Constructor_GeneratesUniqueId()
        {
            // Arrange
            var account1 = ModelHelper.ValidCurrentAccount();
            var account2 = ModelHelper.ValidSavingsAccount();

            // Assert
            Assert.NotEqual(account1.Id, account2.Id);
        }

        [Fact]
        public void Account_DefaultIsFrozen_IsFalse()
        {
            // Arrange
            var account = ModelHelper.ValidCurrentAccount();

            // Assert
            Assert.False(account.IsFrozen);
        }
    }
}
