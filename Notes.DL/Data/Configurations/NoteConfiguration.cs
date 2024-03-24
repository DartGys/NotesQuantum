using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.DL.Data.Entities;

namespace Notes.DL.Data.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Text) 
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .IsRequired();
        }
    }
}
