using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Read_store;

public class Read_context: DbContext
{
    public Read_context(DbContextOptions<Read_context> options): 
        base(options)
    { }

    public DbSet<Box_overview> Box_overviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Box_overview>()
            .HasKey(o => o.Box_id);
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