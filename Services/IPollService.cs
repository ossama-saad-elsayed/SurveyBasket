using Microsoft.AspNetCore.SignalR;
using SurveyBasket.Entities;

namespace SurveyBasket.Services
{
    public interface IPollService
    {
       Task< IEnumerable<Poll> > GetAllAsync(CancellationToken cancellationToken = default);

       Task< Poll?> GetAsync(int id,CancellationToken cancellationToken = default);
        Task <Poll> AddAsync(Poll request, CancellationToken cancellationToken = default);

      Task < bool> UpdateAsync(int id, Poll request, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);

        Task<bool> TogglePublishAsync(int id, CancellationToken cancellationToken = default);
    }
}
