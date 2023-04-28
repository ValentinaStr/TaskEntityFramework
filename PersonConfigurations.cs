using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
	public class PersonConfigurations : IEntityTypeConfiguration<Person>
	{
		public void Configure(EntityTypeBuilder<Person> builder)
		{
			builder.Property(x => x.FirstName).HasMaxLength(10);
			builder.Property(x => x.LastName).IsRequired().HasMaxLength(6);
		}
	}
}

