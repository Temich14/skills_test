using Microsoft.AspNetCore.Mvc;
using skills_test.Adapters.Controllers.Middlewares;
using skills_test.Application.DTO;
using skills_test.Core;
using skills_test.Domain.Ports;
using Swashbuckle.AspNetCore.Annotations;

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

    [HttpPost("/persons")]
    [SwaggerOperation(
        Summary = "Создать нового пользователя",
        Description =
            "Создаёт нового пользователя на основе переданных данных. Имя и отображаемое имя должны быть не пустыми, а уровень навыков должен быть от 1 до 10 включительно."
    )]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePerson([FromBody] PersonRequestDto personDto)
    {
        var result = await _personService.CreatePersonAsync(personDto);

        if (!result.IsSuccess)
        {
            if (result.Error == null)
            {
                throw new ErrorNullException();
            }

            return BadRequest(new ErrorResponse(result.Error));
        }

        var createdPerson = result.Data;

        if (createdPerson == null)
        {
            return StatusCode(500, new ErrorResponse("Internal error occured"));
        }

        return CreatedAtAction(nameof(GetPerson), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut("/persons/{id:long}")]
    [SwaggerOperation(
        Summary = "Обновить данные пользователя",
        Description =
            "Полностью обновляет данные существующего пользователя. Имя и отображаемое имя долждны быть не пустыми, а уровень навыков должен быть от 1 до 10 включительно."
    )]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePerson(long id, [FromBody] PersonRequestDto personDto)
    {
        var result = await _personService.UpdatePersonAsync(id, personDto);

        if (!result.IsSuccess)
        {
            if (result.Error == null)
            {
                throw new ErrorNullException();
            }

            return NotFound(new ErrorResponse(result.Error));
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Удалить пользователя по ID.
    /// </summary>
    [HttpDelete("/persons/{id:long}")]
    [SwaggerOperation(
        Summary = "Удаляет пользователя",
        Description = "Удаляет пользователя если он существует"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var result = await _personService.DeletePersonAsync(id);

        if (!result.IsSuccess)
        {
            if (result.Error == null)
            {
                throw new ErrorNullException();
            }

            return NotFound(new ErrorResponse(result.Error));
        }

        return Ok();
    }

    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    [HttpGet("/persons")]
    [SwaggerOperation(
        Summary = "Получить всех пользователей",
        Description = "получает всех пользователей с их навыками"
    )]
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
    [SwaggerOperation(
        Summary = "Получить пользователя",
        Description = "получает информацию о существующем пользователе"
    )]
    [ProducesResponseType(typeof(PersonResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPerson(long id)
    {
        var result = await _personService.GetPersonByIdAsync(id);

        if (!result.IsSuccess)
        {
            if (result.Error == null)
            {
                throw new ErrorNullException();
            }

            return NotFound(new ErrorResponse(result.Error));
        }

        return Ok(result.Data);
    }
}