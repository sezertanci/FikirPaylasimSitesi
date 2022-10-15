using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations.EntryConfigurations
{
    public class EntryTagConfiguration : BaseEntityConfiguration<EntryTag>
    {
        public override void Configure(EntityTypeBuilder<EntryTag> builder)
        {
            base.Configure(builder);

            builder.ToTable("EntryTag", FikirPaylasimSitesiContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.Entry)
                .WithMany(x => x.EntryTags)
                .HasForeignKey(x => x.EntryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.EntryTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
