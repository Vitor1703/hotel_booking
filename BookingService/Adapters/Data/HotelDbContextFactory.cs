using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class HotelDbContextFactory : IDesignTimeDbContextFactory<HotelDbContext>
{
    public HotelDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();

        // Defina a conexão com o PostgreSQL
        optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=teste123");

        return new HotelDbContext(optionsBuilder.Options);
    }
}
