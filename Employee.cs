namespace EntityFramework
{	public class Employee
	{
		public int Id { get; set; }
		public string? OfficialDuties { get; set; }
		public List<Person> Persons { get; set; } = new();		
	}
}
