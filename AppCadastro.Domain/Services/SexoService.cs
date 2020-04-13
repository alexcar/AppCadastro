using AppCadastro.Domain.Loggin;
using AppCadastro.Domain.Mappers;
using AppCadastro.Domain.Repositories;
using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Domain.Services
{
	public class SexoService : ISexoService
	{
		private readonly ISexoRepository _sexoRepository;
		private readonly ISexoMapper _sexoMapper;
		private readonly ILogger<ISexoService> _logger;

		public SexoService(
			ISexoRepository sexoRepository,
			ISexoMapper sexoMapper,
			ILogger<ISexoService> logger
			)
		{
			_sexoRepository = sexoRepository;
			_sexoMapper = sexoMapper;
			_logger = logger;
		}

		public async Task<IEnumerable<SexoResponse>> GetSexoAsync()
		{
			var result = await _sexoRepository.GetAsync();
			return result.Select(p => _sexoMapper.Map(p)) ;
		}

		public async Task<SexoResponse> GetSexoByIdAsync(GetSexoRequest request)
		{
			if (request?.SexoId == null) throw new ArgumentException();
			var entity = await _sexoRepository.GetAsync(request.SexoId);

			_logger.LogInformation(
				Events.GetById,
				Messages.TargetEntityChanged_id, entity?.SexoId);

			return _sexoMapper.Map(entity);
		}

		public async Task<SexoResponse> AddSexoAsync(AddSexoRequest request)
		{
			var sexo = _sexoMapper.Map(request);
			var result = _sexoRepository.Add(sexo);

			var registrosModificados = await _sexoRepository
				.UnitOfWork.SaveChangesAsync();

			_logger.LogInformation(
				Events.Add,
				Messages.NumberOfRecordAffected_modifiedRecords, registrosModificados);

			_logger.LogInformation(
				Events.Add,
				Messages.ChangesApplied_id, result?.SexoId);

			return _sexoMapper.Map(result);
		}				
	}
}
