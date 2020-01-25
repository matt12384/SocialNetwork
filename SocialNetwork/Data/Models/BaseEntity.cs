namespace SocialNetwork.Data.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
