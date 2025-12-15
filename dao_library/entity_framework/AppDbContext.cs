using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments{ get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Ban> Bans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .OnDelete(DeleteBehavior.Cascade);

       
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Reactions)
            .WithOne(r => r.Post)
            .OnDelete(DeleteBehavior.Cascade);


        
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Creator)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Creator)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.Creator)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.Post) 
            .WithMany(p => p.Reactions)
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Follower>()
            .HasOne(f => f.FollowerUser)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Follower>()
            .HasOne(f => f.FollowedUser)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }


}


