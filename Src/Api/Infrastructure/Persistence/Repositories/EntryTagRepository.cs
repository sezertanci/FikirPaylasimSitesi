using Application.Interfaces.Repositories;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class EntryTagRepository : GenericRepository<EntryTag>, IEntryTagRepository
    {
        public EntryTagRepository(FikirPaylasimSitesiContext context) : base(context)
        {
        }
    }
}
