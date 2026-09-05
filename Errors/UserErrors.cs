using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors
{
    public class UserErrors
    {
        public static readonly Error InvalidCredentials = new ("User.InvalidCredentials", "invaild password or email", StatusCodes.Status400BadRequest);
        public static readonly Error InvalidToken =       new ("User.InvalidCredentials", "Invalid Token", StatusCodes.Status404NotFound);

    }
}
