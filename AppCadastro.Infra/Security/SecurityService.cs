using System;
using System.Security.Cryptography;
using System.Text;
using AppCadastro.Domain.Security;

namespace AppCadastro.Infra.Security
{
	public class SecurityService : ISecurityService
	{
		public Tuple<string, string> GeraSaltHash(string senha)
		{
			var salt = Convert.ToBase64String(GeraSalt());
			var hash = GeraHashComSalt(senha, salt);

			return Tuple.Create(salt, hash);
		}

		public bool ValidaSaltHash(string senha, string salt, string atualHash)
		{
			var hash = GeraHashComSalt(senha, salt);

			if (hash == atualHash)
				return true;
			else
				return false;
		}		

		private byte[] GeraSalt()
		{
			const int MinSaltSize = 4;
			const int MaxSaltSize = 8;

			Random random = new Random();
			int saltSize = random.Next(MinSaltSize, MaxSaltSize);

			byte[] saltBytes = new byte[saltSize];

			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

			rng.GetNonZeroBytes(saltBytes);

			return saltBytes;
		}

		private string GeraHashComSalt(string input, string salt)
		{
			HashAlgorithm algorithm = new MD5CryptoServiceProvider();

			byte[] plainTextBytes = Encoding.UTF8.GetBytes(input);

			byte[] saltBytes = Convert.FromBase64String(salt);

			byte[] plainTextWithSaltBytes =
				new byte[plainTextBytes.Length + saltBytes.Length];

			for (int i = 0; i < plainTextBytes.Length; i++)
				plainTextWithSaltBytes[i] = plainTextBytes[i];

			for (int i = 0; i < saltBytes.Length; i++)
				plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

			byte[] hashBytes = algorithm.ComputeHash(plainTextWithSaltBytes);

			return Convert.ToBase64String(hashBytes);
		}
	}
}
