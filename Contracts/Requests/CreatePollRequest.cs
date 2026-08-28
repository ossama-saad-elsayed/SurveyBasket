using SurveyBasket.Contracts.Responses;
using SurveyBasket.Models;

namespace SurveyBasket.Contracts.Requests
{
    public record CreatePollRequest (string title, string description);
    
}
