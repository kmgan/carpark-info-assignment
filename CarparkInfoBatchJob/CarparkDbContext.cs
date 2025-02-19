using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Diagnostics;
using System.IO;

public class CarparkDbContext : DbContext
{
    public DbSet<Carpark> Carparks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var projectPath = AppDomain.CurrentDomain.BaseDirectory;
        var rootPath = Directory.GetParent(projectPath).Parent.Parent.Parent.Parent.FullName;
        var databasePath = Path.Combine(rootPath, "carparks.db");
        Debug.WriteLine($"Using database at: {databasePath}");
        optionsBuilder
            .UseSqlite($"Data Source={databasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carpark>(entity =>
        {
            entity.HasKey(e => e.car_park_no);
            entity.Property(e => e.car_park_no).IsRequired();
            entity.Property(e => e.address).IsRequired();
            entity.Property(e => e.x_coord).IsRequired();
            entity.Property(e => e.y_coord).IsRequired();
            entity.Property(e => e.car_park_type).IsRequired();
            entity.Property(e => e.type_of_parking_system).IsRequired();
            entity.Property(e => e.short_term_parking).IsRequired();
            entity.Property(e => e.free_parking).IsRequired();
            entity.Property(e => e.night_parking).IsRequired();
            entity.Property(e => e.car_park_decks).IsRequired();
            entity.Property(e => e.gantry_height).IsRequired();
            entity.Property(e => e.car_park_basement).IsRequired();
        });
    }
}
