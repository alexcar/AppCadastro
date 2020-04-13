using AppCadastro.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace AppCadastro.Api.Filters
{
	public class UsuarioExisteAttribute : TypeFilterAttribute
	{
		public UsuarioExisteAttribute() : base(typeof(UsuarioExisteAttributeImpl))
		{
		}

		public class UsuarioExisteAttributeImpl : IAsyncActionFilter
		{
			private readonly IUsuarioService _usuarioService;

			public UsuarioExisteAttributeImpl(IUsuarioService usuarioService)
			{
				_usuarioService = usuarioService;
			}
			
			public async Task OnActionExecutionAsync(
				ActionExecutingContext context, ActionExecutionDelegate next)
			{
				if (!(context.ActionArguments["id"] is int id))
				{
					context.Result = new BadRequestResult();
					return;
				}

				var result = await _usuarioService.GetUsuarioAsync(
					new Domain.Requests.GetUsuarioRequest { UsuarioId = id });

				if (result == null)
				{
					context.Result = new NotFoundObjectResult(
						$"Usuário com id {id} não existe.");
					return;
				}

				await next();
			}
		}
	}
}
