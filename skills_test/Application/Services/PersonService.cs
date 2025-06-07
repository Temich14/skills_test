using skills_test.Application.DTO;
using skills_test.Application.Mapper;
using skills_test.Core;
using skills_test.Domain.Ports;

namespace skills_test.Application.Services;

public class PersonService(IPersonRepository personRepository, IPersonMapper mapper) : IPersonService
{
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly IPersonMapper _mapper = mapper;


    public async Task<Result<PersonDto>> CreatePersonAsync(PersonDto personDto)
    {
        var person = _mapper.MapToPerson(personDto);

        var newPerson = await _personRepository.CreatePerson(person);

        return Result<PersonDto>.Success(_mapper.MapToPersonDto(newPerson));
    }

    public async Task<Result<PersonDto>> UpdatePersonAsync(PersonDto personDto)
    {
        var person = _mapper.MapToPerson(personDto);

        var updatePerson = await _personRepository.UpdatePerson(person);

        if (updatePerson == null)
        {
            return Result<PersonDto>.Failure("Person not found");
        }

        return Result<PersonDto>.Success(_mapper.MapToPersonDto(updatePerson));
    }

    public async Task<Result<bool>> DeletePersonAsync(long id)
    {
        var isDeleted = await _personRepository.DeletePerson(id);
        if (!isDeleted)
        {
            return Result<bool>.Failure("Person not found");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<PersonDto>> GetPersonByIdAsync(long id)
    {
        var person = await _personRepository.GetPerson(id);

        if (person == null)
        {
            return Result<PersonDto>.Failure("Person not found");
        }

        return Result<PersonDto>.Success(_mapper.MapToPersonDto(person));
    }

    public async Task<Result<PersonDto[]>> GetAllPersonsAsync()
    {
        var persons = await _personRepository.GetAllPersons();

        var personDtos = persons
            .Select(p => _mapper.MapToPersonDto(p));

        return Result<PersonDto[]>.Success(personDtos.ToArray());
    }
}