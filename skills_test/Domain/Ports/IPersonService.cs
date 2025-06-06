using skills_test.Application.DTO;
using skills_test.Core;
using skills_test.Domain.Models;

namespace skills_test.Domain.Ports;

public interface IPersonService
{
    Task<Result<PersonDto>> CreatePersonAsync(PersonDto person);

    Task<Result<PersonDto>> UpdatePersonAsync(PersonDto person);

    Task<Result<bool>> DeletePersonAsync(long id);

    Task<Result<PersonDto>> GetPersonByIdAsync(long id);
    Task<Result<PersonDto[]>> GetAllPersonsAsync();
}