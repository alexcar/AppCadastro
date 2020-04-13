using System;
using System.Threading;
using System.Threading.Tasks;
using AppCadastro.Domain.Services;
using FluentValidation;
using System.Globalization;

namespace AppCadastro.Domain.Requests.Validators
{
	public class AddUsuarioRequestValidator : AbstractValidator<AddUsuarioRequest>
	{
		private readonly ISexoService _sexoService;

		public AddUsuarioRequestValidator(ISexoService sexoService)
		{
			_sexoService = sexoService;

			RuleFor(p => p.SexoId)
				.NotEmpty().MustAsync(SexoExiste).WithMessage("Sexo obrigatório");

			RuleFor(p => p.Nome)
				.NotEmpty().WithMessage("Nome obrigatório")
				.MinimumLength(3).WithMessage("Tamanho mínimo permitido de 3 caracteres")
				.MaximumLength(200).WithMessage("Tamanho máximo permitido de 200 caracteres");

			RuleFor(p => p.DataNascimento)
				.NotEmpty().WithMessage("Data de nascimento obrigatório")
				.Must(DataValida).WithMessage("Data de nascimento inválida");

			RuleFor(p => p.Email)
				.NotEmpty().WithMessage("Email obrigatório")
				.EmailAddress().WithMessage("Email inválido")
				.MaximumLength(100).WithMessage("Tamanho máximo permitido de 100 caracteres");

			RuleFor(p => p.Senha)
				.NotEmpty().WithMessage("Senha obrigatório")
				.MaximumLength(30).WithMessage("Tamanho máximo permitido de 30 caracteres");
		}

		private async Task<bool> SexoExiste(int sexoId, CancellationToken cancellationToken)
		{
			if (sexoId <= 0) return false;

			var sexo = await _sexoService
				.GetSexoByIdAsync(new GetSexoRequest { SexoId = sexoId });

			return sexo != null;
		}

		private bool DataValida(DateTime dataNascimento)
		{
			DateTime fromDateValue;
			var formats = new[] { "dd/MM/yyyy 00:00:00", "yyyy-MM-dd 00:00:00" };

			if (DateTime.TryParseExact(
				dataNascimento.ToString(), formats,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None, out fromDateValue))

				return true;
			else
				return false;
		}
	}
}
