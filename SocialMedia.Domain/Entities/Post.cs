namespace SocialMedia.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Likes { get; set; } 
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<LikePost> LikePosts { get; set; }
    }
}
