using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll() => Ok(_pollService.GetAll());


        [HttpGet("{id}")]
        public IActionResult Get(int id) { 
            var poll = _pollService.Get(id);
            return poll is null ? NotFound() : Ok(poll);
        }

        [HttpPost("")]
        public IActionResult Add(Poll Request)
        {
            var Newpoll  = _pollService.Add(Request);
            return CreatedAtAction(nameof(Get), new { ID = Newpoll.Id }, Request);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id ,Poll Request)
        {
            
            var isUpdated  = _pollService.Update(id, Request);

            if (!isUpdated)
                return NotFound();
                
            

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDelete = _pollService.Delete(id);

            if (!isDelete)
                return NotFound();



            return NoContent();
        }

    }
}
