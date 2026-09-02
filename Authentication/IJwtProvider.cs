using SurveyBasket.Entities;

namespace SurveyBasket.Authentication
{
    public interface IJwtProvider
    {

        (string Token, int ExpiresIn) GenerateToken(User user);
        string ? ValidateToken (string token);
    }
}
