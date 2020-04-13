using AppCadastro.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCadastro.Domain.Repositories
{
	public interface ISexoRepository :IRepository
	{
		Task<IEnumerable<Sexo>> GetAsync();
		Task<Sexo> GetAsync(int id);
		Sexo Add(Sexo sexo);
	}
}
