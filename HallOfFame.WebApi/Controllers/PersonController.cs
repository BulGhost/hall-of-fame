using HallOfFame.BusinessLogic.Persons.Commands.CreatePerson;
using HallOfFame.BusinessLogic.Persons.Commands.DeletePerson;
using HallOfFame.BusinessLogic.Persons.Commands.UpdatePerson;
using HallOfFame.BusinessLogic.Persons.Queries.GetAllPersons;
using HallOfFame.BusinessLogic.Persons.Queries.GetPersonById;
using HallOfFame.Domain.Entities;
using HallOfFame.WebApi.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets the list of all persons
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     GET /api/v1/persons
    /// </remarks>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The list of persons</returns>
    /// <response code="200">Success</response>
    [HttpGet("~/api/v{version:apiVersion}/persons")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var query = new GetPersonsQuery();
        var persons = await _mediator.Send(query, cancellationToken);
        return Ok(persons);
    }

    /// <summary>
    /// Gets the required person by Id
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     GET /api/v1/person/[id]
    /// </remarks>
    /// <param name="id">Person Id</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Person with the specified id</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If the person with the specified id is not found</response>
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken = default)
    {
        var query = new GetPersonByIdQuery(id);
        Person person = await _mediator.Send(query, cancellationToken);
        return Ok(person);
    }

    /// <summary>
    /// Adds new person to the database
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     POST /api/v1/person
    ///     {
    ///         "name": "Kristopher Black",
    ///         "displayName": "K_Black",
    ///         "skills":
    ///         [
    ///             {
    ///                 "name": "C#",
    ///                 "level": 8
    ///             },
    ///             {
    ///                 "name": ".NET",
    ///                 "level": 7
    ///             },
    ///             {
    ///                 "name": "EF Core",
    ///                 "level": 6
    ///             }
    ///         ]
    ///     }
    /// </remarks>
    /// <param name="command">Person to add</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A newly created person id</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If the person to be added are invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreatePersonCommand command, CancellationToken cancellationToken = default)
    {
        long personId = await _mediator.Send(command, cancellationToken);
        return Ok(personId);
    }

    /// <summary>
    /// Updates person with the specified ids
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     PUT /api/v1/person/[id]
    ///     {
    ///         "name": "Kristopher White",
    ///         "displayName": "K_White",
    ///         "skills":
    ///         [
    ///             {
    ///                 "name": "C#",
    ///                 "level": 9
    ///             },
    ///             {
    ///                 "name": ".NET",
    ///                 "level": 8
    ///             }
    ///         ]
    ///     }
    /// </remarks>
    /// <param name="id">Person id to update</param>
    /// <param name="person">Person data to update</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>No content</returns>
    /// <response code="204">Success</response>
    /// <response code="400">If the person to be added data are invalid</response>
    /// <response code="404">If the person with the specified id is not found</response>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(long id, [FromBody] PersonDto person, CancellationToken cancellationToken = default)
    {
        var command = new UpdatePersonCommand(id, person.Name, person.DisplayName, person.Skills);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes person with the specified ids
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     DELETE /api/v1/person/[id]
    /// </remarks>
    /// <param name="id">Person id to delete</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>No content</returns>
    /// <response code="204">Success</response>
    /// <response code="404">If the person with the specified id is not found</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
    {
        var command = new DeletePersonCommand(id);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}