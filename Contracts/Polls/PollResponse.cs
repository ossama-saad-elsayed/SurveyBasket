namespace SurveyBasket.Contracts.Polls
{
    public record PollResponse (
        int Id, 
        string title, 
        string Summary, 
        bool IsPublished, 
        DateOnly StartAt,
        DateOnly EndAt);
    
}
