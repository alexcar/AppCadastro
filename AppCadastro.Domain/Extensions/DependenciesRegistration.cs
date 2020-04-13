using AppCadastro.Domain.Mappers;
using AppCadastro.Domain.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppCadastro.Domain.Extensions
{
	public static class DependenciesRegistration
	{
		public static IServiceCollection AddMappers(this IServiceCollection services)
		{
			services
				.AddSingleton<IUsuarioMapper, UsuarioMapper>()
				.AddSingleton<ISexoMapper, SexoMapper>();

			return services;
		}

		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services
				.AddScoped<IUsuarioService, UsuarioService>()
				.AddScoped<ISexoService, SexoService>();

			return services;
		}

		public static IMvcBuilder AddValidation(this IMvcBuilder builder)
		{
			builder
				.AddFluentValidation(configuration =>
					configuration.RegisterValidatorsFromAssembly
					(Assembly.GetExecutingAssembly()));

			return builder;
		}
	}
}
