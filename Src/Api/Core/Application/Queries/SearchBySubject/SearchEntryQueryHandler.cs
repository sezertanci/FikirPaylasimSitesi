using Application.Interfaces.Repositories;
using Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            if(request.SearchText.Length < 3)
                throw new Exception("Text length must be at least 3 characters!");

            var result = entryRepository.Get(x => EF.Functions.Like(x.Subject, $"%{request.SearchText}%"))
                .Select(x => new SearchEntryViewModel()
                {
                    Subject = x.Subject,
                    Id = x.Id
                });

            return await result.Take(5).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}