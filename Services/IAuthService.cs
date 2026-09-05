using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Authentication;

namespace SurveyBasket.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email ,string password,CancellationToken cancellation = default);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token ,string refreshToken,CancellationToken cancellation = default);
        Task<Result> RevokTokenAsync(string token ,string refreshToken,CancellationToken cancellation = default);
    }
} 
