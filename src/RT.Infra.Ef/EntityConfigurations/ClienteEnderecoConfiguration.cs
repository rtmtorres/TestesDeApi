using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RT.Domain.Models;

namespace RT.Infra.Ef.EntityConfigurations
{
    public class ClienteEnderecoConfiguration : IEntityTypeConfiguration<ClienteEndereco>
    {
        public void Configure(EntityTypeBuilder<ClienteEndereco> builder)
        {
            builder.ToTable("TB_CLIENTE_ENDERECO");
            builder.HasKey(t => new { t.ClienteId, t.EnderecoId });
            builder.Property(t => t.ClienteId).HasColumnName("TB_CLIENTE_ID").IsRequired();
            builder.Property(t => t.EnderecoId).HasColumnName("TB_ENDERECO_ID").IsRequired();

            builder.HasOne(t => t.Cliente)
                    .WithMany(t => t.Enderecos)
                    .HasForeignKey(fk => fk.ClienteId);

            builder.HasOne(t => t.Endereco)
                    .WithOne()
                    .IsRequired()
                    .HasForeignKey<ClienteEndereco>(t => t.EnderecoId);
        }
    }
}
