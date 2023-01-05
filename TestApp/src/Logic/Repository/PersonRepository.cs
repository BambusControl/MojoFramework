using MojoFramework.Attributes;
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
			new("Josh", new DateOnly(1987, 2, 12)),
			new("Scarlett", new DateOnly(1992, 10, 7)),
			new("Nancy", new DateOnly(1978, 6, 25)),
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
