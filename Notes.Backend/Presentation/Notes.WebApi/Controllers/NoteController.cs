using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers;
[Produces("Application/json")]
[Route("api/[controller]")]
public class NoteController : BaseController
{
    private readonly IMapper _mapper;

    public NoteController(IMapper mapper) =>
        _mapper = mapper;

    /// <summary>
    /// Gets the list of notes
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note
    /// </remarks>
    /// <returns>Returns NoteListVm</returns>
    /// <response code ="200">Success</response>
    /// <response code="401">If the user unautorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        var query = new GetNoteListQuery
        {
            UserId = UserId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
    {
        var query = new GetNotesDetailsQuery
        {
            UserId = UserId,
            Id = id
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = UserId;
        var noteId = await Mediator.Send(command);
        return Ok(noteId);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateNoteDto updateNoteDto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteNoteCommand
        {
            Id = id,
            UserId = UserId
        };
        await Mediator.Send(command);
        return NoContent();
    }
}