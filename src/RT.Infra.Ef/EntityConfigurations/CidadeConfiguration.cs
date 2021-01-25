using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RT.Domain.Models;

namespace RT.Infra.Ef.EntityConfigurations
{
    public class CidadeConfiguration : IEntityTypeConfiguration<CidadeAggregate>
    {
        public void Configure(EntityTypeBuilder<CidadeAggregate> builder)
        {
            builder.ToTable("TB_CIDADE");
            builder.HasKey(t => t.CidadeId);
            builder.Property(t => t.CidadeId).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.Property(t => t.Nome).HasColumnName("NOME").HasColumnType("varchar").HasMaxLength(100).IsRequired();

            builder.OwnsOne(q => q.Uf, uf=> {
                uf.Property(t => t.Sigla).HasColumnName("ESTADO").HasColumnType("char").HasMaxLength(2).IsRequired();
            });
        }
    }
}
