using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.Persistence.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Title).IsRequired().HasMaxLength(140);
            builder.Property(n => n.Descriprion).IsRequired();
            builder.Property(n => n.Priority).IsRequired();
            builder.Property(n => n.CreatedDate).HasPrecision(0);
            builder.Property(n => n.ModifiedDate).HasPrecision(0);
        }
    }
}
