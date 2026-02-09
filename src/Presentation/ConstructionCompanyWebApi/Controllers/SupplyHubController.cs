using Application.DTOs;
using Application.Features.SupplyHubFeatures.ChangeSupplyHubParent;
using Application.Features.SupplyHubFeatures.CreateSupplyHub;
using Application.Features.SupplyHubFeatures.DeleteSupplyHub;
using Application.Features.SupplyHubFeatures.GetSupplyHub;
using Application.Features.SupplyHubFeatures.GetTree;
using Application.Features.SupplyHubFeatures.GetTreeById;
using Application.Features.SupplyHubFeatures.UpdateSupplyHub;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionCompanyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplyHubController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SupplyHubController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SupplyHubDTO>> GetById(Guid id)
        {
            return await _mediator.Send(new GetSupplyHubQuery { Id = id });
        }

        [HttpGet("tree")]
        [AllowAnonymous]
        public async Task<ActionResult<List<SupplyHubDTO>>> GetFullTree()
        {
            return await _mediator.Send(new GetSupplyHubTreeQuery());
        }

        [HttpGet("{id}/tree")]
        [AllowAnonymous]
        public async Task<ActionResult<SupplyHubDTO>> GetSubTree(Guid id)
        {
            return await _mediator.Send(new GetTreeByIdQuery { Id = id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<SupplyHubDTO>> Create([FromBody] CreateSupplyHubCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSupplyHubCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}/parent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeParent(Guid id, [FromBody] ChangeSupplyHubParentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteSupplyHubCommand { Id = id });
            return NoContent();
        }
    }
}
