using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCadastro.Domain.Services
{
	public interface ISexoService
	{
		Task<IEnumerable<SexoResponse>> GetSexoAsync();
		Task<SexoResponse> GetSexoByIdAsync(GetSexoRequest request);
		Task<SexoResponse> AddSexoAsync(AddSexoRequest request);
	}
}
