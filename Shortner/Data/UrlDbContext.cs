using Microsoft.EntityFrameworkCore;

namespace Shortner;

public class UrlDbContext : DbContext
{
    public UrlDbContext() { }
    public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
    {

    }
    public DbSet<ShortendUrl> ShortendUrls { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=Test.db;");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortendUrl>(builder =>
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Code).IsUnique();
            builder.Property(u => u.Code)
                   .HasMaxLength(UrlShorteningService.NumberOfCharsInShortLink)
                   .IsRequired();
        });
    }
}