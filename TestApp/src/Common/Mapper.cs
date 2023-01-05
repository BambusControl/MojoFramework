using TestApp.Data.Entity;
using TestApp.Data.Model;

namespace TestApp.Common;

public static class Mapper
{
	public static PersonModel ToPersonModel(this PersonEntity person)
	{
		return new PersonModel(person.Name, person.BirthDate);
	}
}
