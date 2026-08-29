using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Entities;
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

        public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default) => await _context.Polls.FindAsync(id, cancellationToken);

        public async Task<Poll> AddAsync(Poll request, CancellationToken cancellationToken = default)
        {
           await  _context.Polls.AddAsync(request, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request;
        }

        public async Task <bool> UpdateAsync(int id, Poll request, CancellationToken cancellationToken = default)
        {
            var poll = await GetAsync(id, cancellationToken);

            if (poll == null)
                return false;

            poll.Title = request.Title;
            poll.Summary = request.Summary;
            poll.StartAt = request.StartAt;
            poll.EndAt = request.EndAt;
              
             await   _context.SaveChangesAsync(cancellationToken);
            return true;


        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var poll = await GetAsync(id, cancellationToken);

            if (poll == null)
                return false;

             _context.Polls.Remove(poll);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> TogglePublishAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll = await GetAsync(id, cancellationToken);

            if (poll == null)
                return false;

            poll.IsPublished = !poll.IsPublished;
            

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
