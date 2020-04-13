using AppCadastro.Domain.Entities;
using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;

namespace AppCadastro.Domain.Mappers
{
	public class SexoMapper : ISexoMapper
	{
		public SexoResponse Map(Sexo sexo)
		{
			if (sexo == null) return null;

			return new SexoResponse
			{
				SexoId = sexo.SexoId,
				Descricao = sexo.Descricao
			};			
		}

		public Sexo Map(AddSexoRequest request)
		{
			if (request == null) return null;

			return new Sexo
			{
				Descricao = request.Descricao
			};
		}
	}
}
