using skills_test.Application.DTO;
using skills_test.Core;
using skills_test.Domain.Models;

namespace skills_test.Domain.Ports;

public interface IPersonService
{
    Task<Result<PersonResponseDto>> CreatePersonAsync(PersonRequestDto personDto);

    Task<Result<PersonResponseDto>> UpdatePersonAsync(long id, PersonRequestDto personDto);

    Task<Result<bool>> DeletePersonAsync(long id);

    Task<Result<PersonResponseDto>> GetPersonByIdAsync(long id);
    Task<Result<List<PersonResponseDto>>> GetAllPersonsAsync();
}