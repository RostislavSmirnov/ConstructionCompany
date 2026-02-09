using Application.DTOs;
using Application.Features.BuildingObjectFeatures.CreateBuildingObject;
using Application.Features.BuildingObjectFeatures.DeleteBuildingObject;
using Application.Features.BuildingObjectFeatures.GetAllBuildingObject;
using Application.Features.BuildingObjectFeatures.GetBuildingObject;
using Application.Features.BuildingObjectFeatures.UpdateBuildingObject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionCompanyWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BuildingObjectController : ControllerBase
{
    private readonly IMediator _mediator;
    public BuildingObjectController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<BuildingObjectDTO>>> GetAll()
    {
        return await _mediator.Send(new GetAllBuildingobjectQuery());
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<BuildingObjectDTO>> GetById(Guid id)
    {
        return await _mediator.Send(new GetBuildingObjectQuery { Id = id });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BuildingObjectDTO>> Create([FromBody] CreateBuildingObjectCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBuildingObjectCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteBuildingObjectCommand { Id = id });
        return NoContent();
    }
}
