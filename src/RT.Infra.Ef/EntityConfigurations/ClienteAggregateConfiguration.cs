using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RT.Domain.Models;

namespace RT.Infra.Ef.EntityConfigurations
{

    public class ClienteAggregateConfiguration : IEntityTypeConfiguration<ClienteAggregate>
    {
        public void Configure(EntityTypeBuilder<ClienteAggregate> builder)
        {
            builder.ToTable("TB_CLIENTE");
            builder.HasKey(t => t.ClienteId);
            builder.Property(t => t.ClienteId).HasColumnName("ID").ValueGeneratedOnAdd();
            builder.OwnsOne(q => q.Nome,
                nome =>
                {
                    nome.Property(t => t.Valor).HasColumnName("NOME").HasMaxLength(200).IsRequired();
                });

            builder.Property(t => t.Rg).HasColumnName("RG").HasMaxLength(20).IsRequired();
          
            builder.OwnsOne(q => q.Cpf,
                nome =>
                {
                    nome.Property(t => t.Numero).HasColumnName("CPF").HasMaxLength(20).IsRequired();
                });
            
            builder.Property(t => t.DataNascimento).HasColumnName("DATA_NASCIMENTO").HasColumnType("Date").IsRequired();
            builder.Property(t => t.Email).HasColumnName("EMAIL_").HasMaxLength(150).IsRequired();
            builder.Property(t => t.Empresa).HasConversion<int>().HasColumnName("COD_EMPRESA").HasMaxLength(150).IsRequired();


            builder.HasMany(t => t.Enderecos)
                .WithOne(t => t.Cliente)
                .HasForeignKey(fk => fk.ClienteId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Ignore(t => t.EnderecosParaRemover);

            builder.Metadata
                .FindNavigation("Enderecos")
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

    }
}
