using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations.EntryConfigurations
{
    public class EntryVoteConfiguration : BaseEntityConfiguration<EntryVote>
    {
        public override void Configure(EntityTypeBuilder<EntryVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("EntryVote", FikirPaylasimSitesiContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.Entry)
                .WithMany(x => x.EntryVotes)
                .HasForeignKey(x => x.EntryId);
        }
    }
}
