using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Models;
namespace SurveyBasket.Services
{
    public class PollService:IPollService
    {
        private static readonly List<Poll> _polls = [
           new Poll {
                Id = 1,
                title = "test",
                description = "test2",
            } ];

        public IEnumerable<Poll> GetAll() => _polls;

        public Poll ? Get(int id) => _polls.SingleOrDefault(x=>x.Id==id);

        public Poll  Add (Poll request )
        {
            request.Id = _polls.Count+1;
            _polls.Add(request);
            return request;
        }

        public bool Update  (int id,Poll request)
        {
            var poll = _polls.SingleOrDefault( x=>x.Id==id);

            if (poll == null)
                return false;

            poll.title = request.title;
            poll.description = request.description;

            return true;


        }

        public bool Delete(int id)
        {
            var poll = _polls.SingleOrDefault(x => x.Id == id);

            if (poll == null)
                return false;

            _polls.Remove(poll);

            return true;
        }

    }

}
