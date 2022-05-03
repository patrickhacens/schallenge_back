using SChallengeAPI.Domain;

namespace SChallengeAPI.Data;

/// <summary>
/// Database Context
/// </summary>
public class Db : DbContext
{
    /// <summary>
    /// Users reference
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Events reference
    /// </summary>
    public DbSet<Event> Events { get; set; }

    /// <summary>
    /// Constructor with options
    /// </summary>
    /// <param name="options"></param>
    public Db(DbContextOptions options) : base(options)
    {
    }

    ///// <summary>
    ///// Constructor without options
    ///// </summary>
    //public Db()
    //{
    //}

    /// <summary>
    /// OnModelCreating override
    /// </summary>
    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.Entity<User>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Id).ValueGeneratedOnAdd();

            e.HasIndex(d => d.Username).IsUnique();
        });

        mb.Entity<Event>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Id).ValueGeneratedOnAdd();

            e.HasOne(d => d.Creator)
                .WithMany(d => d.Events)
                .HasForeignKey(d => d.CreatorId);
        });
    }
}
