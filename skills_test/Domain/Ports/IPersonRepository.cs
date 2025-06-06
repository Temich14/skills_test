using skills_test.Core;
using skills_test.Domain.Models;

namespace skills_test.Domain.Ports;

public interface IPersonRepository
{
    Task<Person> CreatePerson(Person person);
    Task<Person?> UpdatePerson(Person person);
    Task<bool> DeletePerson(long id);
    Task<Person?> GetPerson(long id);
    Task<Person[]?> GetAllPersons();
}