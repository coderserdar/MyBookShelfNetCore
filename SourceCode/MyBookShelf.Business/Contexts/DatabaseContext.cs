using Microsoft.EntityFrameworkCore;
using MyBookShelf.Business.Entities;

namespace MyBookShelf.Business.Contexts;

public class DatabaseContext : DbContext
{
    public string DbPath { get; }

    public DatabaseContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "MyBookShelf.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
    }
    
    public DbSet<User> Users { get; set; }
}