using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Mappings;

public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.ToTable("Book");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(x => x.Author).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(x => x.Link).HasColumnType("varchar").HasMaxLength(255);
    }
}