using MojoFramework.Attributes.Configuration;
using TestApp.Data.Entity;

namespace TestApp.Logic.Repository;

[Component]
public sealed class PersonRepository
{
	private readonly List<PersonEntity> people;

	public PersonRepository()
	{
		people = new List<PersonEntity>
		{
			new("Josh", new DateOnly(year: 1987, month: 2, day: 12)),
			new("Scarlett", new DateOnly(year: 1992, month: 10, day: 7)),
			new("Nancy", new DateOnly(year: 1978, month: 6, day: 25)),
		};
	}

	public PersonEntity? GetByName(string name)
	{
		return people.FirstOrDefault(person => person.Name == name);
	}

	public IEnumerable<PersonEntity> FindByName(string name)
	{
		return people.Where(person => person.Name == name);
	}

	public IEnumerable<PersonEntity> GetByBirthDate(DateOnly birthDate)
	{
		return people.Where(person => person.BirthDate == birthDate);
	}
}
