using Microsoft.AspNetCore.SignalR;
using SurveyBasket.Models;

namespace SurveyBasket.Services
{
    public interface IPollService
    {
        IEnumerable<Poll> GetAll();

        Poll? Get( int id);
        Poll Add(Poll request);

        bool Update ( int id, Poll request);

        bool Delete(int id);
    }
}
