namespace Company.AccountService.Domain.Models.Base
{
    public abstract class DataBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public Guid? CreatedByUserId { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedByUserId { get; set; }
    }
}
