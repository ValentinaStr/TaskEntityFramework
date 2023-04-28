using Amazon.CloudWatchLogs.Model;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;

namespace EntityFramework
{
	public class ApplicationContext : DbContext
	{
		private static ApplicationContext? _instance;
		public DbSet<Employee>? Employee { get; set; } = null!;
		public DbSet<Person>? Person { get; set; } = null!;
		public DbSet<Address>? Address { get; set; } = null!;

		readonly StreamWriter sw = new(@"D:\projects\dotnet\EntityFramework\Log.txt", true);
		private ApplicationContext()
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}
		public static ApplicationContext GetApplicationContext()
		{
			_instance ??= new ApplicationContext();
			return _instance;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");			
			optionsBuilder.LogTo(sw.WriteLine); 
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new PersonConfigurations());
		}
	}

}
