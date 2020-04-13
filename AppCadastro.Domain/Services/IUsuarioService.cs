using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCadastro.Domain.Services
{
	public interface IUsuarioService
	{
		Task<IEnumerable<UsuarioResponse>> GetUsuariosAsync();
		Task<UsuarioResponse> GetUsuarioAsync(GetUsuarioRequest request);
		Task<IEnumerable<UsuarioResponse>> GetUsuarioBySexoIdAsync(GetUsuarioBySexoIdRequest request);
		Task<IEnumerable<UsuarioResponse>> Search(SearchUsuarioRequest request);
		Task<UsuarioResponse> GetUsuarioByEmailAsync(GetUsuarioByEmailRequest request);
		Task<UsuarioResponse> AddUsuarioAsync(AddUsuarioRequest request);
		Task<UsuarioResponse> EditUsuarioAsync(EditUsuarioRequest request);
		Task DeleteUsuarioAsync(DeleteUsuarioRequest request);
	}
}
