using Microsoft.EntityFrameworkCore;
using CarparkInfoAPI.Models;
using System;
using System.Diagnostics;
using System.IO;

namespace CarparkInfoAPI.Data
{
	public class CarparkDbContext : DbContext
	{
		public CarparkDbContext(DbContextOptions<CarparkDbContext> options) : base(options) { }

		public DbSet<Carpark> Carparks { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<UserFavourite> UserFavourites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Get base directory of the running application
			var projectPath = AppDomain.CurrentDomain.BaseDirectory;
			var rootPath = Directory.GetParent(projectPath)?.Parent?.Parent?.Parent?.Parent?.FullName;

			// Resolve the path to the SQLite database
			var databasePath = Path.Combine(rootPath ?? "", "carparks.db");

			// Debug output to verify the resolved database path
			Debug.WriteLine($"[CarparkDbContext] Using database at: {databasePath}");

			// Set up SQLite connection
			optionsBuilder.UseSqlite($"Data Source={databasePath}");
		}
	}
}
