using SurveyBasket.Contracts.Responses;
using SurveyBasket.Entities;

namespace SurveyBasket.Contracts.Requests
{
    public record CreatePollRequest (
       string Title,
        string Summary,
        bool IsPublished,
        DateOnly StartAt,
        DateOnly EndAt);

}
