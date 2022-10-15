using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class EntryCommentFavoriteRepository : GenericRepository<EntryCommentFavorite>, IEntryCommentFavoriteRepository
    {
        public EntryCommentFavoriteRepository(FikirPaylasimSitesiContext context) : base(context)
        {
        }
    }
}
