using Microsoft.EntityFrameworkCore;

public class AppDataConext : DbContext
{
    public DbSet<Folha> Folhas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=LucasDeAlbuquerque.db");
    }
}