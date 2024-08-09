using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Domain.Entities;
using System.Data;

namespace SocialMedia.Infrastructure.Persistence
{
    public class SocialMediaDbContext : DbContext, ISocialMediaDbContext
    {

        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<LikePost> LikePosts { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasKey(e => e.Id)
                    .HasName("PK_User");

                entity.Property(e => e.Username)
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.HashedPassword)
                    .HasColumnType("CHAR")
                    .HasMaxLength(128)
                    .IsRequired();

                entity.Property(e => e.FullName)
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasKey(e => e.Id)
                    .HasName("PK_Post");

                entity.HasOne(e => e.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_Post_User_UserId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Content)
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.Property(e => e.Image);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("DATETIME")
                    .IsRequired();

                entity.Property(e => e.Likes)
                    .HasColumnType("INT")
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<LikePost>(entity =>
            {
                entity.ToTable("LikePost");

                entity.HasKey(e => e.Id)
                    .HasName("PK_LikePost");

                entity.HasOne(e => e.User)
                    .WithMany(p => p.LikePosts)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_LikePost_User_UserId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Post)
                    .WithMany(p => p.LikePosts)
                    .HasForeignKey(e => e.PostId)
                    .HasConstraintName("FK_LikePost_Post_PostId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Islike)
                    .HasColumnType("BIT")
                    .IsRequired();

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("DATETIME")
                    .IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasKey(e => e.Id)
                    .HasName("PK_Comment");

                entity.HasOne(e => e.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_Comment_User_UserId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(e => e.PostId)
                    .HasConstraintName("FK_Comment_Post_PostId")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("DATETIME")
                    .IsRequired();
            });
        }
    }
}
