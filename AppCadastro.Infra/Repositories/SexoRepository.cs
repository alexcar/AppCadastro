using AppCadastro.Domain.Entities;
using AppCadastro.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Infra.Repositories
{
	public class SexoRepository : ISexoRepository
	{
		private readonly AppCadastroContext _context;
		public IUnitOfWork UnitOfWork => _context;

		public SexoRepository(AppCadastroContext context)
		{
			_context = context ?? throw new ArgumentException(nameof(context));
		}

		public async Task<IEnumerable<Sexo>> GetAsync()
		{
			return await _context
				.Sexos
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Sexo> GetAsync(int id)
		{
			return await _context
				.Sexos
				.AsNoTracking()
				.Where(p => p.SexoId == id)				
				.FirstOrDefaultAsync();
		}

		public Sexo Add(Sexo sexo)
		{
			return _context.Sexos.Add(sexo).Entity;
		}		
	}
}
