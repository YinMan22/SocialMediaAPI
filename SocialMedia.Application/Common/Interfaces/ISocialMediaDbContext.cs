using SocialMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Application.Common.Interfaces
{
    public interface ISocialMediaDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<LikePost> LikePosts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
