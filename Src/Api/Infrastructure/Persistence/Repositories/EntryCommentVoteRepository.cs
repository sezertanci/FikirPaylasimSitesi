using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class EntryCommentVoteRepository : GenericRepository<EntryCommentVote>, IEntryCommentVoteRepository
    {
        public EntryCommentVoteRepository(FikirPaylasimSitesiContext context) : base(context)
        {
        }
    }
}
