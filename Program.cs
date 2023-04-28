using EntityFramework;

LoggerService logger = LoggerService.GetLogger();
using ApplicationContext db = ApplicationContext.GetApplicationContext();

var employee1 = new Employee { OfficialDuties = "Teacher" };
var employee2 = new Employee { OfficialDuties = "Director" };

var address1 = new Address { City = "Minsk", Street = "Moskovskaya", House = 63,  };
var address2 = new Address { City = "Brest", Street = "Gogolia", House = 00 };
var address3 = new Address { City = "Grodno", Street = "Moskovskaya", House = 63 };
var address4 = new Address { City = "Minsk", Street = "Moskovskaya", House = 88 };
var address5 = new Address { City = "Gomel", Street = "Moskovskaya", House = 63, };
var address6 = new Address { City = "Vilnius", Street = "Gogolia", House = 00 };
var address7 = new Address { City = "Moskva", Street = "Moskovskaya", House = 63 };
var address8 = new Address { City = "Kiev", Street = "Moskovskaya", House = 88 };

var person1 = new Person { FirstName = "Alex", LastName = "Ivanov", Employee = employee1 };
var person2 = new Person { LastName = "Petrov", FirstName = "Alex", Employee = employee1 };
var person3 = new Person { LastName = "Sidor", FirstName = "Semen", Employee = employee1 };
var person4 = new Person { LastName = "Sidor", FirstName = "Boss", Employee = employee2 };

db.Employee.Add(employee1);
db.SaveChanges();

db.Employee.Add(employee2);
db.SaveChanges();

db.Person.AddRange(person1, person2);
db.Person.Add(person3);
db.Person.Add(person4);
db.SaveChanges();

person1.Address.Add(address1);
person1.Address.Add(address5);
person2.Address.Add(address3);
person2.Address.Add(address6);
person3.Address.Add(address4);
person3.Address.Add(address7);
person3.Address.Add(address2);
person4.Address.Add(address8);

db.SaveChanges();

try
{
	//db.Person.Add(person4);
	//db.Person.Add(person3);
	//db.SaveChanges();
}
catch	
{ 
	logger.WriteLogAsync($"{person4.LastName} not correct last name!!!");
	Console.WriteLine($"{person4.LastName} not correct last name!!!");
}

// получаем объекты из бд и выводим на консоль
var users = db.Person.ToList();
Console.WriteLine("List of person:");
foreach (Person u in users)
{
	Console.WriteLine($"{u.Id} {u.FirstName} {u.LastName}");
}

Console.WriteLine("________________");

var selectList = users.Where(x => x.LastName != "Sid")
	.Select(u => (u.Employee.OfficialDuties, u.FirstName, u.LastName, u.Address[0]?.City)).ToList();

foreach (var e in selectList)
{
	Console.WriteLine(e.OfficialDuties + ' ' + e.FirstName + " " + e.LastName + " " + e.City);
}

var personsWithAddresses = db.Person.Join(db.Address, // второй набор
		   p => p.Id, // свойство-селектор объекта из первого набора
		   a => a.PersonId, // свойство-селектор объекта из второго набора
		   (p, a) => new // результат
		   {
			   
			   PersonFirstName = p.FirstName,
			   PersonLastName = p.LastName,
			   AddressCity = a.City,
			   AddressStreet = a.Street,
			   AddressNumber = a.House
		   });
foreach (var p in personsWithAddresses)
	Console.WriteLine($"Name: {p.PersonFirstName}  {p.PersonLastName}, Address: {p.AddressCity} - {p.AddressStreet}, {p.AddressNumber}");
