using FluentValidation;

namespace SurveyBasket.Contracts.Authentication
{
    public class RefreshTokenRequsetValidator: AbstractValidator<RefreshTokenRequest>
    {

      public  RefreshTokenRequsetValidator ()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
            RuleFor(x=>x.Token).NotEmpty();
        }
    }
}
