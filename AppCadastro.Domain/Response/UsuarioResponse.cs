using AppCadastro.Domain.Entities;
using System;

namespace AppCadastro.Domain.Response
{
	public class UsuarioResponse
	{
		public int UsuarioId { get; set; }
		public string Nome { get; set; }
		public string DataNascimento { get; set; }
		public string Email { get; set; }
		public string Senha { get; set; }
		public bool Ativo { get; set; }
		public int SexoId { get; set; }
		public SexoResponse Sexo { get; set; }
	}
}
