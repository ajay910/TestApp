namespace TestApp.Domain
{
    using SQLite;

    public abstract class BaseEntity
    {
        [PrimaryKey, MaxLength(36)]
        public string Id { get; set; }
    }
}