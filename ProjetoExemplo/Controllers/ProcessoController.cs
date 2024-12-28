using Microsoft.AspNetCore.Mvc;
using ProjetoExemplo.Models;
using ProjetoExemplo.Repositories;
using ProjetoExemplo.Services;
using System.Diagnostics;

namespace ProjetoExemplo.Controllers
{
    public class ProcessoController : Controller
    {
        private readonly IProcessoRepository repository;
        private readonly IbgeApiService ibgeApiService;

        public ProcessoController(IProcessoRepository repository, IbgeApiService ibgeApiService)
        {
            this.repository = repository;
            this.ibgeApiService = ibgeApiService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 5;

            var processosQuery = repository.Query();
            var paginatedProcessos = await PaginatedList<Processo>.CreateAsync(processosQuery, page, pageSize);

            return View(paginatedProcessos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Processo processo)
        {
            if (ModelState.IsValid)
            {
                processo.DataCadastro = DateTime.UtcNow;

                await repository.AddAsync(processo);
                return RedirectToAction(nameof(Index));
            }
            return View(processo);
        }

        public async Task<IActionResult> Details(int id)
        {
            var processo = await repository.GetByIdAsync(id);
            if (processo == null)
            {
                return NotFound();
            }

            if (processo.DataVisualizacao == null)
            {
                processo.DataVisualizacao = DateTime.UtcNow;
                await repository.UpdateAsync(processo);
            }

            var timeZone = TimeZoneInfo.Local;
            ViewBag.DataVisualizacaoLocal = TimeZoneInfo.ConvertTimeFromUtc(processo.DataVisualizacao.Value, timeZone);

            ViewBag.Ufs = new[] { "SP", "RJ", "MG", "BA", "PR" };

            return View(processo);
        }

        public IActionResult Create() => View();

        [HttpGet]
        public async Task<JsonResult> GetMunicipios(string uf)
        {
            var municipios = await ibgeApiService.GetMunicipiosByUf(uf);
            return Json(municipios);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var processo = await repository.GetByIdAsync(id);
            if (processo == null)
            {
                return NotFound();
            }

            return View(processo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Processo processo)
        {
            if (id != processo.Id)
            {
                return NotFound();
            }

            var existingProcess = await repository.GetByIdAsync(id);
            if (existingProcess == null)
            {
                return NotFound("O processo com o ID especificado não foi encontrado.");
            }

            if (!string.IsNullOrWhiteSpace(processo.Name))
                existingProcess.Name = processo.Name;

            if (!string.IsNullOrWhiteSpace(processo.Npu))
                existingProcess.Npu = processo.Npu;

            if (!string.IsNullOrWhiteSpace(processo.Uf))
                existingProcess.Uf = processo.Uf;

            if (!string.IsNullOrWhiteSpace(processo.MunicipioNome))
                existingProcess.MunicipioNome = processo.MunicipioNome;

            await repository.UpdateAsync(existingProcess);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var processo = await repository.GetByIdAsync(id);
            if (processo == null)
            {
                return NotFound();
            }

            await repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
