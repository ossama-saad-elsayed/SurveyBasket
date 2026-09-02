using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contracts.Polls;
using SurveyBasket.Entities;
using SurveyBasket.Services;
using System.Threading.Tasks;

namespace SurveyBasket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PollsController : ControllerBase
    {
        private readonly  IPollService _pollService;
     public  PollsController (IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet("GetAll")]
        public async Task <IActionResult> GetAll(CancellationToken cancellationToken)
        { 
        var polls = await _pollService.GetAllAsync(cancellationToken);
        var response = polls.Adapt<IEnumerable<PollResponse>>();
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
        {
            var poll = await _pollService.GetAsync(id, cancellationToken);

            if (poll is null)
                return NotFound();

            var response = poll.Adapt<PollResponse>();

            return Ok(response);
        }

        [HttpPost("")]
        public async Task < IActionResult> Add([FromBody] CreatePollRequest Request, CancellationToken cancellationToken)
        {
            var Newpoll = await _pollService.AddAsync(Request.Adapt<Poll>(), cancellationToken);
            return CreatedAtAction(nameof(Get), new { ID = Newpoll.Id }, Newpoll.Adapt<PollResponse>());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreatePollRequest Request, CancellationToken cancellationToken)
        {

            var isUpdated =  await _pollService.UpdateAsync(id, Request.Adapt<Poll>(),  cancellationToken);

            if (!isUpdated)
                return NotFound();



            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task< IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken )
        {
            var isDelete =  await _pollService.Delete(id, cancellationToken);

            if (!isDelete)
                return NotFound();



            return NoContent();
        }

        [HttpPut("{id}/TogglePublish")]
        public async Task<IActionResult> TogglePublish([FromRoute] int id,  CancellationToken cancellationToken)
        {

            var isUpdated = await _pollService.TogglePublishAsync(id,cancellationToken);

            if (!isUpdated)
                return NotFound();



            return NoContent();
        }
    }
}
