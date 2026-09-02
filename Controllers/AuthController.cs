using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Services;
using SurveyBasket.Contracts.Authentication;
namespace SurveyBasket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Login([FromBody] LoginRequset request, CancellationToken cancellation)
        {
            var Loginresponse = await _authService.GetTokenAsync(request.Email, request.Password, cancellation);

            if (Loginresponse == null)
                return BadRequest("invaild password or email");

            return Ok(Loginresponse);
        }
        [HttpPost("refresh")]

        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellation)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token,request.RefreshToken, cancellation);

            if (authResult == null)
                return BadRequest("invaild Token ");

            return Ok(authResult);
        }

        [HttpPut("revok-refresh-token")]
        public async Task<IActionResult> RevokRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellation)
        {
            var authResult = await _authService.RevokTokenAsync(request.Token, request.RefreshToken, cancellation);

            if (authResult == false)
                return BadRequest("invaild Token ");

            return Ok();
        }
    }
}
