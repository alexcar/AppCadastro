using System.Collections.Generic;

namespace AppCadastro.Domain.Entities
{
	public class Sexo
	{
		public int SexoId { get; set; }
		public string Descricao { get; set; }

		public ICollection<Usuario> Usuarios { get; set; }
	}
}
