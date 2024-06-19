using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");


            builder.HasKey(i => i.Id);

            builder.Property(t => t.Title).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(80);
            
            builder.Property(d=> d.Description).IsRequired(false).HasColumnType("NVARCHAR").HasMaxLength(300);
            
            builder.Property(u => u.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(200);
        }
    }
}
