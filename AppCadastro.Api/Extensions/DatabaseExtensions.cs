using AppCadastro.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppCadastro.Api.Extensions
{
	public static class DatabaseExtensions
	{
		public static IServiceCollection AddAppCadastroContext(
			this IServiceCollection services, string connectionString)
		{
			return services
				.AddEntityFrameworkSqlServer()
				.AddDbContext<AppCadastroContext>(contextOptions =>
				{
					contextOptions.UseSqlServer(
						connectionString,
						serverOptions =>
						{
							serverOptions.MigrationsAssembly
								(typeof(Startup).Assembly.FullName);
						});
				});
		}
	}
}
