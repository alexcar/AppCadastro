using AppCadastro.Domain.Entities;
using AppCadastro.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Infra.Repositories
{
	public class UsuarioRepository : IUsuarioRepository
	{
		private readonly AppCadastroContext _context;
		public IUnitOfWork UnitOfWork => _context;		

		public UsuarioRepository(AppCadastroContext context)
		{
			_context = context ?? throw new ArgumentException(nameof(context));
		}

		public async Task<IEnumerable<Usuario>> GetAsync()
		{
			return await _context
				.Usuarios
				.AsNoTracking()
				.Include(p => p.Sexo)
				.ToListAsync();				
		}

		public async Task<Usuario> GetAsync(int id)
		{
			return await _context
				.Usuarios
				.AsNoTracking()
				.Where(p => p.UsuarioId == id)
				.Include(p => p.Sexo)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Usuario>> GetUsuarioBySexoIdAsync(int id)
		{
			return await _context
				.Usuarios
				.AsNoTracking()
				.Where(p => p.SexoId == id)
				.Include(p => p.Sexo)
				.ToListAsync();
		}

		public async Task<IEnumerable<Usuario>> Search(string nome, bool? ativo)
		{
			return await _context
				.Usuarios
				.AsNoTracking()
				.Include(p => p.Sexo)
				.Where(p => String.IsNullOrEmpty(nome) || p.Nome.Contains(nome))
				.Where(p => ativo == null || p.Ativo == ativo)
				.ToListAsync();
		}

		public async Task<Usuario> GetUsuarioByEmailAsync(string email)
		{
			return await _context
				.Usuarios
				.AsNoTracking()
				.Where(p => p.Email == email)
				.FirstOrDefaultAsync();
		}

		public Usuario Add(Usuario usuario)
		{
			return _context.Usuarios.Add(usuario).Entity;
		}						

		public Usuario Update(Usuario usuario)
		{
			_context.Entry(usuario).State = EntityState.Modified;
			return usuario;
		}

		public void Delete(Usuario usuario)
		{
			_context.Entry(usuario).State = EntityState.Deleted;
		}		
	}
}
