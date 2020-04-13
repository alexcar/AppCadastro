using System;

namespace AppCadastro.Api.Filters
{
	public class JsonErrorPayload
	{
		public int EventId { get; set; }
		public string[] Messages { get; set; }
		public Object DetailedMessage { get; set; }
	}
}
