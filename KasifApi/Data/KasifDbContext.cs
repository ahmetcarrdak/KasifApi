using Microsoft.EntityFrameworkCore;
using KasifApi.Models;

namespace KasifApi.Data;

public class KasifDbContext : DbContext
{
    public KasifDbContext(DbContextOptions<KasifDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<EventRegistion> EventRegistions { get; set; }
    public DbSet<Following> Followings { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostSaved> PostSaveds { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<Notification> Notifications { get; set; }
}