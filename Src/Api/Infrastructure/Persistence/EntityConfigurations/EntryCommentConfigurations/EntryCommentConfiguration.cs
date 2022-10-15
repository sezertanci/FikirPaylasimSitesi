using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations.EntryCommentConfigurations
{
    public class EntryCommentConfiguration : BaseEntityConfiguration<EntryComment>
    {
        public override void Configure(EntityTypeBuilder<EntryComment> builder)
        {
            base.Configure(builder);

            builder.ToTable("EntryComment", FikirPaylasimSitesiContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.Entry)
                .WithMany(x => x.EntryComments)
                .HasForeignKey(x => x.EntryId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.EntryComments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
