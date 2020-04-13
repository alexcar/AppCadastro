using AppCadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCadastro.Infra.SchemaDefinitions
{
	public class SexoEntitySchemaDefinitions : IEntityTypeConfiguration<Sexo>
	{
		public void Configure(EntityTypeBuilder<Sexo> builder)
		{
			builder.ToTable("Sexo");
			builder.HasKey(p => p.SexoId);
			builder.Property(p => p.SexoId);
			
			builder.Property(p => p.Descricao)
				.IsRequired()
				.HasMaxLength(15);
		}
	}
}
