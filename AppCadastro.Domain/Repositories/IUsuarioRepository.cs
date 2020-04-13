using AppCadastro.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCadastro.Domain.Repositories
{
	public interface IUsuarioRepository : IRepository
	{
		Task<IEnumerable<Usuario>> GetAsync();
		Task<Usuario> GetAsync(int id);
		Usuario Add(Usuario usuario);
		Usuario Update(Usuario usuario);
		Task<IEnumerable<Usuario>> GetUsuarioBySexoIdAsync(int id);
		Task<IEnumerable<Usuario>> Search(string nome, bool? ativo);
		Task<Usuario> GetUsuarioByEmailAsync(string email);
		void Delete(Usuario usuario);
	}
}
