namespace EntityFramework
{
	public class Person
	{
		public int Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }

		public int EmployeeId { get; set; }
		public Employee? Employee { get; set; }

		//public int AddressId { get; set; }
		public List<Address> Address { get; set; } = new();
	}
}
