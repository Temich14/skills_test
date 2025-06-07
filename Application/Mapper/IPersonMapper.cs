using skills_test.Application.DTO;
using skills_test.Domain.Models;

namespace skills_test.Application.Mapper;

public interface IPersonMapper
{
    PersonResponseDto MapToPersonDto(Person person);
    Person MapToPerson(PersonRequestDto personDto);
}