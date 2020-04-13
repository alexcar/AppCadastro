using AppCadastro.Domain.Entities;
using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;

namespace AppCadastro.Domain.Mappers
{
	public interface ISexoMapper
	{
		SexoResponse Map(Sexo sexo);
		Sexo Map(AddSexoRequest request);
	}
}
