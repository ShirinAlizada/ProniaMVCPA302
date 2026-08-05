    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProniaMVCPA302.Models;

    namespace ProniaMVCPA302.DAL.SQLConfigurations
    {
        public class ProductConfiguration : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
                builder.ToTable("Products");

                builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(p => p.SKU)
                    .HasMaxLength(50);

                builder.Property(p => p.Description)
                    .HasMaxLength(1000);

                builder.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

                builder.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);

                builder.HasMany(p => p.ProductImages)
                    .WithOne(pi => pi.Product)
                    .HasForeignKey(pi => pi.ProductId);
            }
        }
    }
