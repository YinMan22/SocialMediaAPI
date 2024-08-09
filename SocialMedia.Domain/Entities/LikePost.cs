namespace SocialMedia.Domain.Entities
{
    public class LikePost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool Islike {  get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
