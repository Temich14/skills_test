using skills_test.Application.DTO;
using skills_test.Domain.Models;

namespace skills_test.Application.Mapper;

public interface IPersonMapper
{
    PersonDto MapToPersonDto(Person person);
    Person MapToPerson(PersonDto personDto);
}