using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoExemplo.Models;
using ProjetoExemplo.Repositories;

namespace ProjetoExemplo.Views.Processo
{
    public class IndexModel : PageModel
    {
        private readonly IProcessoRepository repository;

        public IndexModel(IProcessoRepository repository)
        {
            this.repository = repository;
        }

        public List<ProjetoExemplo.Models.Processo> Processos { get; set; }

        public async Task OnGetAsync(int page = 1, int pageSize = 10)
        {
            Processos = await repository.GetPaginatedAsync(page, pageSize);
        }
    }
}
