using SurveyBasket.Contracts.Polls;
using SurveyBasket.Entities;

namespace SurveyBasket.Contracts.Polls
{
    public record CreatePollRequest (
       string Title,
        string Summary,
        DateOnly StartAt,
        DateOnly EndAt);

}
