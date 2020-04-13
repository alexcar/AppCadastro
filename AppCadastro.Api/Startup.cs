using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCadastro.Api.Extensions;
using AppCadastro.Api.Filters;
using AppCadastro.Domain.Extensions;
using AppCadastro.Domain.Repositories;
using AppCadastro.Domain.Security;
using AppCadastro.Infra.Repositories;
using AppCadastro.Infra.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppCadastro.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// services.AddControllersWithViews();
			services
				.AddAppCadastroContext(Configuration.GetSection("DataSource:ConnectionString").Value)
				.AddResponseCaching()
				.AddMemoryCache()
				.AddScoped<IUsuarioRepository, UsuarioRepository>()
				.AddScoped<ISexoRepository, SexoRepository>()
				.AddScoped<ISecurityService, SecurityService>()
				.AddMappers()
				.AddServices()
				.AddControllers()
				.AddValidation()
				.AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

			services.AddMvc(options =>
			{
				options.Filters.Add(new CustomExceptionAttribute());
			});

			services.AddCors(options => options.AddPolicy("ApiCorsPolicy", build =>
			{
				build.WithOrigins("http://localhost:4200")
					 .AllowAnyMethod()
					 .AllowAnyHeader();
			}));

		services
				.AddOpenApiDocument(settings =>
				{
					settings.Title = "App Cadastro Api";
					settings.DocumentName = "V3";
					settings.Version = "V3";
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseHsts();

			app.UseRouting();
			app.UseCors("ApiCorsPolicy");
			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.UseStaticFiles();
			app.UseResponseCaching();			
			app.UseOpenApi().UseSwaggerUi3();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Usuario}/{action=Get}/{id?}");

				// endpoints.MapControllers();
			});
		}
	}
}
