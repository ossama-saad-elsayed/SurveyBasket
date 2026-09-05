using Microsoft.AspNetCore.SignalR;
using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Polls;
using SurveyBasket.Entities;

namespace SurveyBasket.Services
{
    public interface IPollService
    {
       Task< IEnumerable<Poll> > GetAllAsync(CancellationToken cancellationToken = default);

       Task<Result<PollResponse>> GetAsync(int id,CancellationToken cancellationToken = default);
        Task<Result<PollResponse>> AddAsync(CreatePollRequest request, CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(int id, CreatePollRequest request, CancellationToken cancellationToken = default);

        Task<Result> Delete(int id, CancellationToken cancellationToken = default);

        Task<Result> TogglePublishAsync(int id, CancellationToken cancellationToken = default);
    }
}
