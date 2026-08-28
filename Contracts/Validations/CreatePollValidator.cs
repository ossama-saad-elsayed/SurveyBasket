using FluentValidation;
using SurveyBasket.Contracts.Requests;
namespace SurveyBasket.Contracts.Validations
{
    public class CreatePollRequestValidator : AbstractValidator<CreatePollRequest>
    {

        public CreatePollRequestValidator() {

            RuleFor(X=>X.title).NotEmpty(); 
        }
    }
}
