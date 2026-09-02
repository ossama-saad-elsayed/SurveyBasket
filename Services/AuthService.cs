using Microsoft.AspNetCore.Identity;
using SurveyBasket.Authentication;
using SurveyBasket.Contracts.Authentication;
using SurveyBasket.Entities;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace SurveyBasket.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly int _refreshTokenExpiryDays = 14;
        public AuthService(UserManager<User> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }
        public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellation = default)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return null;

            var IsValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!IsValidPassword)
                return null;

            var (token, expiresIn) = _jwtProvider.GenerateToken(user);

            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });
            await _userManager.UpdateAsync(user);
            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, refreshToken, refreshTokenExpiration);
        }

        private static string GenerateRefreshToken()
        {

            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId is null)
                return null;

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return null;


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x=>x.Token==refreshToken&&x.IsActive);
            if (userRefreshToken is null) return null;
                
            userRefreshToken.RevokOn = DateTime.UtcNow;

            var (newtoken, expiresIn) = _jwtProvider.GenerateToken(user);

            var newrefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newrefreshToken,
                ExpiresOn = refreshTokenExpiration
            });
            await _userManager.UpdateAsync(user);
            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newtoken, expiresIn, newrefreshToken, refreshTokenExpiration);
        }

        public async Task<bool?> RevokTokenAsync(string token, string refreshToken, CancellationToken cancellation = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId is null)
                return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
            if (userRefreshToken is null) return false;

            userRefreshToken.RevokOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);


            return true;

        }
    }
}
