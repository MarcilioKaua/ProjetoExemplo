using Microsoft.EntityFrameworkCore;
using ProjetoExemplo.Data;
using ProjetoExemplo.Models;

namespace ProjetoExemplo.Repositories.Implementations
{
    public class ProcessoRepository : IProcessoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProcessoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Processo> Query()
        {
            return _context.Processos.OrderByDescending(p => p.DataCadastro);
        }

        public async Task<List<Processo>> GetPaginatedAsync(int pageIndex, int pageSize)
        {
            return await _context.Processos
                                 .OrderByDescending(p => p.DataCadastro)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task AddAsync(Processo processo)
        {
            _context.Processos.Add(processo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var processo = await _context.Processos.FindAsync(id);
            if (processo != null)
            {
                _context.Processos.Remove(processo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Processo>> GetAllAsync()
        {
            return await _context.Processos.ToListAsync();
        }

        public async Task<Processo> GetByIdAsync(int id)
        {
            return await _context.Processos.FindAsync(id);
        }

        public async Task UpdateAsync(Processo processo)
        {
            _context.Processos.Update(processo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> VerificarNpuExistenteAsync(string npu)
        {
            return await _context.Processos.AnyAsync(p => p.Npu == npu);
        }
    }
}
