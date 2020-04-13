using System;

namespace AppCadastro.Domain.Requests
{
	public class EditUsuarioRequest
	{
		public int UsuarioId { get; set; }
		public string Nome { get; set; }
		public DateTime DataNascimento { get; set; }
		public string Email { get; set; }
		public string Senha { get; set; }
		public int SexoId { get; set; }
		public bool Ativo { get; set; }
	}
}
