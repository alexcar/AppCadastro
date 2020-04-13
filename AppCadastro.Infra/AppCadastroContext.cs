using AppCadastro.Domain.Entities;
using AppCadastro.Domain.Repositories;
using AppCadastro.Infra.SchemaDefinitions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AppCadastro.Infra
{
	public class AppCadastroContext : DbContext, IUnitOfWork
	{
		public AppCadastroContext(DbContextOptions<AppCadastroContext> options) :
			base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UsuarioEntitySchemaDefinitions());
			modelBuilder.ApplyConfiguration(new SexoEntitySchemaDefinitions());
			
			base.OnModelCreating(modelBuilder);
		}
		
		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
		{
			await SaveChangesAsync(cancellationToken);
			return true;
		}

		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Sexo> Sexos { get; set; }
	}

	
}
