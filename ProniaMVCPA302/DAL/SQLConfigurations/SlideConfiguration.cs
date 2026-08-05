using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaMVCPA302.Models;

namespace ProniaMVCPA302.DAL.SQLConfigurations
{
    public class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");

            builder.Property(s => s.Title)
                .HasMaxLength(100);

            builder.Property(s => s.SubTitle)
                .HasMaxLength(100);

            builder.Property(s => s.Description)
                .HasMaxLength(1000);

            builder.Property(s => s.Image)
                .HasMaxLength(500);
        }
    }
}
