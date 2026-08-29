using FluentValidation;
using SurveyBasket.Contracts.Requests;
namespace SurveyBasket.Contracts.Validations
{
    public class CreatePollRequestValidator : AbstractValidator<CreatePollRequest>
    {

        public CreatePollRequestValidator() {

            RuleFor(X => X.Title).NotEmpty()
                .Length(3, 100);
            RuleFor(X => X.Summary).NotEmpty().Length(3, 1500);

            RuleFor(X =>  X.StartAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

            RuleFor(X => X.StartAt)
               .NotEmpty();

            RuleFor(X => X).
                Must(HasVaildDate).
                WithName(nameof(CreatePollRequest.EndAt)).
                WithMessage("{PropertyName} must be greater than  start date");
            
        }

        private bool HasVaildDate(CreatePollRequest createPollRequest)
        {
            return (createPollRequest.EndAt >= createPollRequest.StartAt);
        }
    }
}
