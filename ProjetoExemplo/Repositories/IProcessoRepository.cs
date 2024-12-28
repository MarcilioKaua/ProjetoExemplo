using ProjetoExemplo.Models;

namespace ProjetoExemplo.Repositories
{
    public interface IProcessoRepository
    {
        IQueryable<Processo> Query();
        Task<List<Processo>> GetPaginatedAsync(int pageIndex, int pageSize);
        Task<IEnumerable<Processo>> GetAllAsync();
        Task<Processo> GetByIdAsync(int id);
        Task AddAsync(Processo processo);
        Task UpdateAsync(Processo processo);
        Task DeleteAsync(int id);
    }
}
