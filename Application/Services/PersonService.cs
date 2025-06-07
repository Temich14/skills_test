using skills_test.Application.DTO;
using skills_test.Application.Mapper;
using skills_test.Core;
using skills_test.Domain.Ports;

namespace skills_test.Application.Services;

public sealed class PersonService(
    IPersonRepository personRepository,
    IPersonMapper mapper,
    ILogger<PersonService> logger) : IPersonService
{
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly IPersonMapper _mapper = mapper;
    private readonly ILogger<PersonService> _logger = logger;

    public async Task<Result<PersonResponseDto>> CreatePersonAsync(PersonRequestDto personDto)
    {
        _logger.LogInformation("Creating new person");
        var person = _mapper.MapToPerson(personDto);
        var newPerson = await _personRepository.CreatePerson(person);
        _logger.LogDebug("Person created with ID: {Id}", newPerson.Id);

        return Result<PersonResponseDto>.Success(_mapper.MapToPersonDto(newPerson));
    }

    public async Task<Result<PersonResponseDto>> UpdatePersonAsync(long id, PersonRequestDto personDto)
    {
        _logger.LogInformation("Updating person with ID: {Id}", id);
        var person = _mapper.MapToPerson(personDto);
        person.Id = id;
        var updatedPerson = await _personRepository.UpdatePerson(person);

        if (updatedPerson == null)
        {
            _logger.LogWarning("Person with ID {Id} not found for update", id);
            return Result<PersonResponseDto>.Failure("Person not found");
        }

        _logger.LogDebug("Person with ID {Id} updated successfully", id);
        return Result<PersonResponseDto>.Success(_mapper.MapToPersonDto(updatedPerson));
    }

    public async Task<Result<bool>> DeletePersonAsync(long id)
    {
        _logger.LogInformation("Deleting person with ID: {Id}", id);
        var isDeleted = await _personRepository.DeletePerson(id);

        if (!isDeleted)
        {
            _logger.LogWarning("Person with ID {Id} not found", id);
            return Result<bool>.Failure("Person not found");
        }

        _logger.LogDebug("Person with ID {Id} deleted", id);
        return Result<bool>.Success(true);
    }

    public async Task<Result<PersonResponseDto>> GetPersonByIdAsync(long id)
    {
        _logger.LogInformation("Getting person with ID: {Id}", id);
        var person = await _personRepository.GetPerson(id);

        if (person == null)
        {
            _logger.LogWarning("Person with ID {Id} not found", id);
            return Result<PersonResponseDto>.Failure("Person not found");
        }

        _logger.LogDebug("Got Person with ID {Id}", id);
        return Result<PersonResponseDto>.Success(_mapper.MapToPersonDto(person));
    }

    public async Task<Result<List<PersonResponseDto>>> GetAllPersonsAsync()
    {
        _logger.LogInformation("Getting all persons");
        var persons = await _personRepository.GetAllPersons();

        var personDtos = persons.Select(_mapper.MapToPersonDto).ToList();
        _logger.LogDebug("Got {Count} persons", personDtos.Count());

        return Result<List<PersonResponseDto>>.Success(personDtos);
    }
}