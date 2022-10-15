using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations.EntryConfigurations
{
    public class EntryConfiguration : BaseEntityConfiguration<Entry>
    {
        public override void Configure(EntityTypeBuilder<Entry> builder)
        {
            base.Configure(builder);

            builder.ToTable("Entry", FikirPaylasimSitesiContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Entries)
                .HasForeignKey(x => x.UserId);
        }
    }
}
