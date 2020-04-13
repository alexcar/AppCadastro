using AppCadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCadastro.Infra.SchemaDefinitions
{
	public class UsuarioEntitySchemaDefinitions : IEntityTypeConfiguration<Usuario>
	{
		public void Configure(EntityTypeBuilder<Usuario> builder)
		{
			builder.ToTable("Usuario");
			builder.HasKey(p => p.UsuarioId);
			
			builder.Property(p => p.Nome)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(p => p.DataNascimento)
				.IsRequired();

			builder.Property(p => p.Email)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.Senha)
				.IsRequired()
				.HasMaxLength(30);

			builder.Property(p => p.Ativo)
				.IsRequired();

			builder
				.HasOne(p => p.Sexo)
				.WithMany(p => p.Usuarios)
				.HasForeignKey(p => p.SexoId);

			builder.Property(p => p.Salt)
				.IsRequired()
				.HasMaxLength(90);

			builder.Property(p => p.Hash)
				.IsRequired()
				.HasMaxLength(90);
				
		}
	}
}
