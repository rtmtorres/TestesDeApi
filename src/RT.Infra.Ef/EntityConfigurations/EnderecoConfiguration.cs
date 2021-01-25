using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RT.Domain.Models;

namespace RT.Infra.Ef.EntityConfigurations
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("TB_ENDERECO");
            builder.HasKey(t => t.EnderecoId);
            builder.Property(t => t.EnderecoId).HasColumnName("ID").ValueGeneratedOnAdd();
            builder.Property(t => t.Rua).HasColumnName("RUA").HasColumnType("varchar").HasMaxLength(255).IsRequired();
            builder.Property(t => t.Bairro).HasColumnName("BAIRRO").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(t => t.Numero).HasColumnName("NUMERO").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(t => t.Complemento).HasColumnName("COMPLEMENTO").HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(t => t.Cep).HasColumnName("CEP").HasColumnType("varchar").HasMaxLength(10).IsRequired();
            builder.Property(t => t.TipoEndereco).HasConversion<int>().HasColumnName("TIPO_ENDERECO").IsRequired();
            builder.Property(t => t.CidadeId).HasColumnName("TB_CIDADE_ID").IsRequired();

            builder.HasOne(t => t.Cidade)
                .WithMany()
                .HasForeignKey(fk => fk.CidadeId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
