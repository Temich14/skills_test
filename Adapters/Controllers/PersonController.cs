using Microsoft.AspNetCore.Mvc;
using skills_test.Adapters.Controllers.Middlewares;
using skills_test.Application.DTO;
using skills_test.Domain.Ports;

namespace skills_test.Adapters.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Создать нового пользователя.
    /// </summary>
    [HttpPost("/persons")]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePerson([FromBody] PersonRequestDto personDto)
    {
        var result = await _personService.CreatePersonAsync(personDto);

        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse(result.Error));
        }

        var createdPerson = result.Data;

        if (createdPerson == null)
        {
            return StatusCode(500, new ErrorResponse("Internal error occured"));
        }

        return CreatedAtAction(nameof(GetPerson), new { id = createdPerson.Id }, createdPerson);
    }

    /// <summary>
    /// Обновить пользователя по ID.
    /// </summary>
    [HttpPut("/persons/{id:long}")]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePerson(long id, [FromBody] PersonRequestDto personDto)
    {
        var result = await _personService.UpdatePersonAsync(id, personDto);

        if (!result.IsSuccess)
        {
            return NotFound(new ErrorResponse(result.Error));
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Удалить пользователя по ID.
    /// </summary>
    [HttpDelete("/persons/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var result = await _personService.DeletePersonAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(new ErrorResponse(result.Error));
        }

        return Ok();
    }

    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    [HttpGet("/persons")]
    [ProducesResponseType(typeof(IEnumerable<PersonResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPersons()
    {
        var result = await _personService.GetAllPersonsAsync();

        if (!result.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Получить пользователя по ID.
    /// </summary>
    [HttpGet("/persons/{id:long}")]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPerson(long id)
    {
        var result = await _personService.GetPersonByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(new ErrorResponse(result.Error));
        }

        return Ok(result.Data);
    }
}