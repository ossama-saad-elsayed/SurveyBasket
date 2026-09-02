using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Authentication
{
    public class JwtProvider(IOptions <JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options= options.Value;
        public (string Token, int ExpiresIn) GenerateToken(User user)
        {
            Claim[] claim = [
              new(JwtRegisteredClaimNames.Sub,user.Id),
              new(JwtRegisteredClaimNames.Email,user.Email!),
              new(JwtRegisteredClaimNames.GivenName,user.FirstName),
              new(JwtRegisteredClaimNames.FamilyName,user.LastName),
              new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

                ];


            var symmertricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.key)); 
            var singingCredentials = new SigningCredentials(symmertricSecuritykey,SecurityAlgorithms.HmacSha256);



            var Token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
                signingCredentials: singingCredentials);

            return (Token: new JwtSecurityTokenHandler().WriteToken(Token), ExpiresIn: _options.ExpiryMinutes*60);
        }

        public string? ValidateToken(string token)
        {
            var  tokenHandler  = new JwtSecurityTokenHandler();
            var symmertricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.key));


            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = symmertricSecuritykey,
                    ValidateIssuerSigningKey =true,
                    ValidateAudience =false,
                    ValidateIssuer =false,
                    ClockSkew =TimeSpan.Zero

                },out SecurityToken validatedToken);

                var JwtToken = (JwtSecurityToken)validatedToken;

                return JwtToken.Claims.First(x=>x.Type == JwtRegisteredClaimNames.Sub).Value;


            }
            catch
            {
                return null;
            }

        }
    }
}
