using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Polls;
using SurveyBasket.Entities;
using SurveyBasket.Errors;
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
            var result = await _pollService.GetAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] CreatePollRequest Request, CancellationToken cancellationToken)
        {
            var result = await _pollService.AddAsync(Request, cancellationToken);
            return result.IsSuccess ?  CreatedAtAction(nameof(Get), new { ID = result.Value.Id }, result) :result.ToProblem() ;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreatePollRequest Request, CancellationToken cancellationToken)
        {

            var result =  await _pollService.UpdateAsync(id, Request,  cancellationToken);
            if (result.IsFailure)
                return result.ToProblem();



            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.Delete(id, cancellationToken);

            if (result.IsFailure)
                return result.ToProblem();



            return NoContent();
        }

        [HttpPut("{id}/TogglePublish")]
        public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
        {

            var result = await _pollService.TogglePublishAsync(id, cancellationToken);

            if (result.IsFailure)
                return result.ToProblem();



            return NoContent();
        }
    }
}
