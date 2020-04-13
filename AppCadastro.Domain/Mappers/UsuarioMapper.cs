using AppCadastro.Domain.Entities;
using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;

namespace AppCadastro.Domain.Mappers
{
	public class UsuarioMapper : IUsuarioMapper
	{
		private readonly ISexoMapper _sexoMapper;

		public UsuarioMapper(ISexoMapper sexoMapper)
		{
			_sexoMapper = sexoMapper;
		}
		
		public Usuario Map(AddUsuarioRequest request)
		{
			if (request == null) return null;

			var usuario = new Usuario
			{
				Nome = request.Nome,
				Email = request.Email,
				DataNascimento = request.DataNascimento,
				Senha = request.Senha,
				SexoId = request.SexoId
			};

			return usuario;
		}

		public Usuario Map(EditUsuarioRequest request)
		{
			if (request == null) return null;

			var usuario = new Usuario
			{
				UsuarioId = request.UsuarioId,
				Nome = request.Nome,
				Email = request.Email,
				DataNascimento = request.DataNascimento,
				Senha = request.Senha,
				SexoId = request.SexoId,
				Ativo = request.Ativo
			};

			return usuario;
		}

		public UsuarioResponse Map(Usuario request)
		{
			if (request == null) return null;

			var usuario = new UsuarioResponse
			{
				UsuarioId = request.UsuarioId,
				Nome = request.Nome,
				Email = request.Email,
				DataNascimento = request.DataNascimento.ToString("yyyy-MM-dd"),
				Senha = request.Senha,
				SexoId = request.SexoId,
				Ativo = request.Ativo,
				Sexo = _sexoMapper.Map(request.Sexo)
			};

			return usuario;
		}
	}
}
