using System;

namespace AppCadastro.Domain.Security
{
	public interface ISecurityService
	{
		Tuple<string, string> GeraSaltHash(string senha);
		bool ValidaSaltHash(string senha, string salt, string atualHash);
	}
}
