using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contracts.Requests;
using SurveyBasket.Contracts.Responses;
using SurveyBasket.Models;
using SurveyBasket.Services;

namespace SurveyBasket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly  IPollService _pollService;
     public  PollsController (IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        { 
        var polls = _pollService.GetAll();
        var response = polls.Adapt<IEnumerable<PollResponse>>();
            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult Get( [FromRoute ] int id) { 
            var poll = _pollService.Get(id);

            if (poll is null)
                return NotFound();

            var response = poll.Adapt<PollResponse>();

           return    Ok(response);
        }

        [HttpPost("")]
        public IActionResult Add([FromBody] CreatePollRequest Request)
        {
            var Newpoll  = _pollService.Add(Request.Adapt<Poll>());
            return CreatedAtAction(nameof(Get), new { ID = Newpoll.Id }, Newpoll);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id , [FromBody] CreatePollRequest Request)
        {
            
            var isUpdated  = _pollService.Update(id, Request.Adapt<Poll>());

            if (!isUpdated)
                return NotFound();
                
            

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var isDelete = _pollService.Delete(id);

            if (!isDelete)
                return NotFound();



            return NoContent();
        }

    }
}
