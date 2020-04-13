using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCadastro.Domain.Entities;
using AppCadastro.Domain.Loggin;
using AppCadastro.Domain.Mappers;
using AppCadastro.Domain.Repositories;
using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;
using AppCadastro.Domain.Security;
using Microsoft.Extensions.Logging;

namespace AppCadastro.Domain.Services
{
	public class UsuarioService : IUsuarioService
	{
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly IUsuarioMapper _usuarioMapper;
		private readonly ILogger<IUsuarioService> _logger;
		private readonly ISecurityService _securityService;

		public UsuarioService(
			IUsuarioRepository usuarioRepository,
			IUsuarioMapper usuarioMapper,
			ILogger<IUsuarioService> logger,
			ISecurityService securityService)
		{
			_usuarioRepository = usuarioRepository;
			_usuarioMapper = usuarioMapper;
			_logger = logger;
			_securityService = securityService;
		}

		public async Task<IEnumerable<UsuarioResponse>> GetUsuariosAsync()
		{
			var result = await _usuarioRepository.GetAsync();
			return result.Select(p => _usuarioMapper.Map(p));
		}

		public async Task<UsuarioResponse> GetUsuarioAsync(GetUsuarioRequest request)
		{
			if (request?.UsuarioId == null) throw new ArgumentException();
			var entity = await _usuarioRepository.GetAsync(request.UsuarioId);

			_logger.LogInformation(
				Events.GetById,
				Messages.TargetEntityChanged_id, entity?.UsuarioId);

			return _usuarioMapper.Map(entity);
		}		

		public async Task<IEnumerable<UsuarioResponse>> GetUsuarioBySexoIdAsync(GetUsuarioBySexoIdRequest request)
		{
			if (request?.SexoId == null) throw new ArgumentException();
			
			var result = await _usuarioRepository.GetUsuarioBySexoIdAsync(request.SexoId);
			return result.Select(p => _usuarioMapper.Map(p));
		}

		public async Task<IEnumerable<UsuarioResponse>> Search(SearchUsuarioRequest request)
		{
			var result = await _usuarioRepository.Search(request.Nome, request.Ativo);
			return result.Select(p => _usuarioMapper.Map(p));
		}

		public async Task<UsuarioResponse> GetUsuarioByEmailAsync(GetUsuarioByEmailRequest request)
		{
			if (request?.Email == null) throw new ArgumentException();

			var result = await _usuarioRepository.GetUsuarioByEmailAsync(request.Email);
			return _usuarioMapper.Map(result);
		}

		public async Task<UsuarioResponse> AddUsuarioAsync(AddUsuarioRequest request)
		{
			/*
			 * ####################################
			 *    Observação sobre o campo senha
			 * ####################################
			 * 
			 * Como não é uma boa prática armazenar no banco de dados a senha do usuário,
			 * tomei a iniciativa de também armazenar no banco de dados informações que podem
			 * ser utilizadas na validação da senha sem a necessidade de armazenar
			 * a senha do usuário no banco de dados.
			 * 
			 */

			if (await ValidaEmailDuplicadoInclusaoAsync(request.Email))
				throw new ArgumentException($"Email {request.Email} já cadastrado.");

			var usuario = _usuarioMapper.Map(request);

			// Gera um hash e salt da senha.
			var saltHashSenha = _securityService.GeraSaltHash(request.Senha);
			usuario.Salt = saltHashSenha.Item1;
			usuario.Hash = saltHashSenha.Item2;			

			AtivaUsuario(usuario);			

			var result = _usuarioRepository.Add(usuario);

			var registrosModificados = await _usuarioRepository
				.UnitOfWork.SaveChangesAsync();

			_logger.LogInformation(
				Events.Add,
				Messages.NumberOfRecordAffected_modifiedRecords, registrosModificados);

			_logger.LogInformation(
				Events.Add,
				Messages.ChangesApplied_id, result?.UsuarioId);

			return _usuarioMapper.Map(result);
		}

		public async Task<UsuarioResponse> EditUsuarioAsync(EditUsuarioRequest request)
		{
			var usuario = await _usuarioRepository.GetAsync(request.UsuarioId);

			if (usuario == null)
				throw new ArgumentException($"Usuário com o ID {request.UsuarioId} não existe.");

			if (await ValidaEmailDuplicadoAlteracaoAsync(usuario.Email, request.Email))
				throw new ArgumentException($"Email {request.Email} já cadastrado.");			

			var entity = _usuarioMapper.Map(request);

			// Verifica se a senha foi alterada.
			// Caso afirmativo, gera e salva o novo hash e salt.
			if (!_securityService.ValidaSaltHash(request.Senha, usuario.Salt, usuario.Hash))
			{
				// gera novo hash e salt
				var saltHashSenha = _securityService.GeraSaltHash(request.Senha);
				entity.Salt = saltHashSenha.Item1;
				entity.Hash = saltHashSenha.Item2;
			}
			else
			{
				entity.Salt = usuario.Salt;
				entity.Hash = usuario.Hash;
			}

			var result = _usuarioRepository.Update(entity);

			await _usuarioRepository.UnitOfWork.SaveChangesAsync();

			_logger.LogInformation(
				Events.Edit,
				Messages.ChangesApplied_id, result?.UsuarioId);

			return _usuarioMapper.Map(result);
		}

		public async Task DeleteUsuarioAsync(DeleteUsuarioRequest request)
		{
			var usuario = await _usuarioRepository.GetAsync(request.UsuarioId);

			if (usuario == null)
				throw new ArgumentException($"Usuário com o ID {request.UsuarioId} não existe.");

			_usuarioRepository.Delete(usuario);
			await _usuarioRepository.UnitOfWork.SaveChangesAsync();

			_logger.LogInformation(
				Events.Delete,
				Messages.ChangesApplied_id, request?.UsuarioId);
		}

		/// <summary>
		/// Todo usuário deve ser inserido com o status ativo igual a true.
		/// </summary>
		/// <param name="usuario"></param>
		private void AtivaUsuario(Usuario usuario)
		{
			usuario.Ativo = true;
		}
		
		/// <summary>
		/// Valida se já existe o e-mail no momento da inclusão
		/// </summary>
		/// <param name="usuarioId"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		private async Task<bool> ValidaEmailDuplicadoInclusaoAsync(string email)
		{
			var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);

			if (usuario == null)
				return false;
			else
				return true;
		}

		/// <summary>
		/// Valida se já existe o e-mail no momento da alteração
		/// </summary>
		/// <param name="emailAtual"></param>
		/// <param name="novoEmail"></param>
		/// <returns></returns>
		private async Task<bool> ValidaEmailDuplicadoAlteracaoAsync(
			string emailAtual, string novoEmail)
		{
			if (emailAtual != novoEmail)
			{
				var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(novoEmail);

				if (usuario == null)
					return false;
				else
					return true;
			}
			else
				return false;
		}		
	}
}
