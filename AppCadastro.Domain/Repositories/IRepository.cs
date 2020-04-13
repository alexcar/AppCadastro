
namespace AppCadastro.Domain.Repositories
{
	public interface IRepository
	{
		IUnitOfWork UnitOfWork { get; }
	}
}
