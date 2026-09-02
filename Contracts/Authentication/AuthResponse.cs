namespace SurveyBasket.Contracts.Authentication
{
    public record AuthResponse(
         string ID,
         string? Email
        ,string FirstName
        ,string LastName
        ,string Token
        ,int ExpiresIn,
         string RefreshToken,
         DateTime RefreshTokenExpiration
        );
  
}
