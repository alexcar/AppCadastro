using AppCadastro.Domain.Entities;
using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;

namespace AppCadastro.Domain.Mappers
{
	public interface IUsuarioMapper
	{
		Usuario Map(AddUsuarioRequest request);
		Usuario Map(EditUsuarioRequest request);
		UsuarioResponse Map(Usuario request);		
	}
}
