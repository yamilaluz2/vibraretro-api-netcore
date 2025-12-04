using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rol> Rols { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments{ get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Ban> Bans { get; set; }

}
