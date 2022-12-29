#nullable enable
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8629 // Nullable value type may be null.
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Data;
namespace ASPRad.Models{
	public class AppDBContext : DbContext
	{
	public DbSet<Bid_Players> Bid_Players { get; set; } = null!;
	public DbSet<My_Players> My_Players { get; set; } = null!;
	public DbSet<Permissions> Permissions { get; set; } = null!;
	public DbSet<Register_With_Trophy> Register_With_Trophy { get; set; } = null!;
	public DbSet<Roles> Roles { get; set; } = null!;
	public DbSet<Teams> Teams { get; set; } = null!;
	public DbSet<Trophies> Trophies { get; set; } = null!;
	public DbSet<Users> Users { get; set; } = null!;
		public DbSet<QueryNumCount> NumCount { get; set; } = null!;
		public DbSet<QueryLabelValue> LabelValue { get; set; } = null!;
		public DbSet<QueryLabelValueCount> LabelValueCount { get; set; } = null!;
		public DbSet<QueryValueCount> ValueCount { get; set; } = null!;
		public AppDBContext(DbContextOptions<AppDBContext> options): base(options){ }
		public AppDBContext(){ }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
		
	}
}
