using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Services;
using SurveyBasket.Contracts.Authentication;
using SurveyBasket.Abstractions;
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

            return Loginresponse.IsSuccess ? Ok(Loginresponse.Value) : Loginresponse.ToProblem();





        }
        [HttpPost("refresh")]

        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellation)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token,request.RefreshToken, cancellation);

            if (authResult.IsFailure)
                return authResult.ToProblem();
            return Ok(authResult.Value);
        }

        [HttpPut("revok-refresh-token")]
        public async Task<IActionResult> RevokRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellation)
        {
            var authResult = await _authService.RevokTokenAsync(request.Token, request.RefreshToken, cancellation);

            if (authResult.IsFailure)
                return authResult.ToProblem();

            return Ok();
        }
    }
}
