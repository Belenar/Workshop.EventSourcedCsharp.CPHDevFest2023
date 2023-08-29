using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Read_store;

public class Read_context: DbContext
{
    public Read_context(DbContextOptions<Read_context> options): 
        base(options)
    { }

    public DbSet<Box_overview> Box_overviews { get; set; }
    public DbSet<Projection_checkpoint> Checkpoints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Box_overview>()
            .HasKey(o => o.Box_id);

        modelBuilder.Entity<Projection_checkpoint>()
            .HasKey(p => p.Projection_type);

        modelBuilder.Entity<Projection_checkpoint>()
            .Property(p => p.Projection_type)
            .HasColumnType("varchar")
            .HasMaxLength(64);
    }
}

public class Box_overview
{
    public Guid Box_id { get; set; }
    public int Open_spaces { get; set; }
    public Box_status Status { get; set; }
}

public enum Box_status
{
    Open,
    Closed,
    Sent
}

public class Projection_checkpoint
{
    public string Projection_type { get; set; }
    public ulong Checkpoint { get; set; }
}