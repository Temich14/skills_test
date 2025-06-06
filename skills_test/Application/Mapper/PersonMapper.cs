using skills_test.Application.DTO;
using skills_test.Domain.Models;

namespace skills_test.Application.Mapper;

public class PersonMapper : IPersonMapper
{
    public PersonDto MapToPersonDto(Person person)
    {
        return new PersonDto(person.Id, person.Name, person.DisplayName, person.Skill);
    }

    public Person MapToPerson(PersonDto personDto)
    {
        return new Person(personDto.Id, personDto.Name, personDto.DisplayName, personDto.Skill);
    }
}