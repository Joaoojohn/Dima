using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");


            builder.HasKey(i => i.Id);

            builder.Property(t => t.Title).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(80);

            builder.Property(t => t.Type).IsRequired().HasColumnType("SMALLINT");

            builder.Property(a => a.Amount).IsRequired().HasColumnType("MONEY");
            
            builder.Property(c => c.CreatedAt).IsRequired();

            builder.Property(p => p.PaidOrReceivedAt).IsRequired(false);

            builder.Property(u => u.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(200);
        }
    }
}
