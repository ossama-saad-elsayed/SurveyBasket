using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Polls;
using SurveyBasket.Entities;
using SurveyBasket.Errors;
using SurveyBasket.Persistence;
using System.Threading;
namespace SurveyBasket.Services
{
    public class PollService : IPollService
    {
      readonly private  ApplicationDbContext _context ;

      public PollService (ApplicationDbContext context)
        {  
            _context = context; 
        }

        public async Task < IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken =default) => await _context.Polls.AsNoTracking().ToListAsync(cancellationToken) ;

        public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default) {
            var poll = await _context.Polls.FindAsync(id, cancellationToken);

            return poll is not null
                ? Result.Success(poll.Adapt<PollResponse>())
                : Result.Failure<PollResponse>(PollErrors.NotFound);
                  
        }

        public async Task<Result<PollResponse>> AddAsync(CreatePollRequest request, CancellationToken cancellationToken = default)
        {
            var poll = request.Adapt<Poll>();
            var IsExistingTitle = await _context.Polls.AnyAsync(x=>x.Title ==request.Title);
            if (IsExistingTitle)
                return Result.Failure<PollResponse>(PollErrors.DuplicatedTitle);

            await _context.AddAsync(poll, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(poll.Adapt<PollResponse>()) ;
        }


        public async Task <Result> UpdateAsync(int id, CreatePollRequest request, CancellationToken cancellationToken = default)
        {
            var poll = await _context.Polls.FindAsync(id, cancellationToken); 

            if (poll == null)
                return Result.Failure(PollErrors.NotFound);

            var IsExistingTitle = await _context.Polls.AnyAsync(x => x.Title == request.Title && x.Id !=id);
            if (IsExistingTitle)
                return Result.Failure<PollResponse>(PollErrors.DuplicatedTitle);


            poll.Title = request.Title;
            poll.Summary = request.Summary;
            poll.StartAt = request.StartAt;
            poll.EndAt = request.EndAt;

            await   _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }


        public async Task<Result> Delete(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _context.Polls.FindAsync(id, cancellationToken);
            if (poll == null)
                return Result.Failure(PollErrors.NotFound); 

            _context.Remove(poll);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> TogglePublishAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _context.Polls.FindAsync(id, cancellationToken);

            if (poll == null)
                return Result.Failure(PollErrors.NotFound);

            poll.IsPublished = !poll.IsPublished;


            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

}
