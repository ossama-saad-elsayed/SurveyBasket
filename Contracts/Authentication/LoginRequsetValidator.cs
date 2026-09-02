using FluentValidation;

namespace SurveyBasket.Contracts.Authentication
{
    public class LoginRequsetValidator : AbstractValidator<LoginRequset>
    {

        public LoginRequsetValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x=>x.Password)
                .NotEmpty();  

        }

    }
}
