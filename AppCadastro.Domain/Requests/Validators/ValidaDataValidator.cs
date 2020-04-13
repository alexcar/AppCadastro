using FluentValidation.Validators;
using System;
using System.Globalization;

namespace AppCadastro.Domain.Requests.Validators
{
	//public class ValidaDataValidator<T> : PropertyValidator
	//{
	//	private DateTime _dataNascimento;

	//	public ValidaDataValidator(DateTime dataNascimento) :
	//		base ("Data de nascimento inválida")
	//	{
	//		_dataNascimento = dataNascimento;
	//	}
		
	//	protected override bool IsValid(PropertyValidatorContext context)
	//	{
	//		var formats = new[] { "dd/MM/yyyy  00:00:00", "yyyy-MM-dd  00:00:00" };

	//		if (DateTime.TryParseExact(
	//			_dataNascimento.ToString(), formats,
	//			CultureInfo.InvariantCulture,
	//			DateTimeStyles.None, out DateTime fromDateValue))
	//		{
	//			return true;
	//		}
	//		else
	//		{
	//			return false;
	//		}
	//	}
	//}
}
