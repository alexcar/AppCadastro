using System;

namespace AppCadastro.Domain.Requests
{
	public class AddUsuarioRequest
	{
		public string Nome { get; set; }
		public DateTime DataNascimento { get; set; }
		public string Email { get; set; }
		public string Senha { get; set; }
		public int SexoId { get; set; }
	}
}
