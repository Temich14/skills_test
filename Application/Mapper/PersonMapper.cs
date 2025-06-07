using skills_test.Application.DTO;
using skills_test.Domain.Models;

namespace skills_test.Application.Mapper;

public class PersonMapper : IPersonMapper
{
    public PersonResponseDto MapToPersonDto(Person person)
    {
        var skillsDto = person.Skill.Select(s => new SkillDto(s.Name, s.Level)).ToList();

        return new PersonResponseDto(person.Id, person.Name, person.DisplayName, skillsDto);
    }

    public Person MapToPerson(PersonRequestDto personDto)
    {
        var skills = personDto.Skill.Select(s => new Skill(0, s.Name, s.Level)).ToList();
        return new Person(0, personDto.Name, personDto.DisplayName, skills);
    }
}