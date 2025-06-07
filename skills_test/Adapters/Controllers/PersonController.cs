using Microsoft.AspNetCore.Mvc;
using skills_test.Application.DTO;
using skills_test.Domain.Ports;

namespace skills_test.Adapters.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost("/persons")]
    public async Task<IActionResult> CreatePerson([FromBody] PersonDto personDto)
    {
        var result = await _personService.CreatePersonAsync(personDto);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        var createdPerson = result.Data;

        if (createdPerson == null)
        {
            return StatusCode(500, "Internal server error");
        }

        return CreatedAtAction(nameof(GetPerson), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut("/persons/{id:long}")]
    public async Task<IActionResult> UpdatePerson(long id, [FromBody] PersonDto personDto)
    {
        personDto.Id = id;

        var result = await _personService.UpdatePersonAsync(personDto);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpDelete("/persons/{id:long}")]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var result = await _personService.DeletePersonAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok();
    }

    [HttpGet("/persons")]
    public async Task<IActionResult> GetAllPersons()
    {
        var result = await _personService.GetAllPersonsAsync();

        if (!result.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(result.Data);
    }

    [HttpGet("/persons/{id:long}")]
    public async Task<IActionResult> GetPerson(long id)
    {
        var result = await _personService.GetPersonByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Data);
    }
}