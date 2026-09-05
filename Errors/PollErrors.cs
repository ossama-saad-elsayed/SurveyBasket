using SurveyBasket.Abstractions;
namespace SurveyBasket.Errors
{
    public  static class PollErrors
    {
        public static readonly Error NotFound = new ("Poll Not Found", "no poll with given ID",StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedTitle = new("Duplicated Title", "Failed To Preform there is anthor poll with this  Title", StatusCodes.Status409Conflict);

    }
}
