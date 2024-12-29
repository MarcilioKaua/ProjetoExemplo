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
        private readonly ProcessoService processoService;

        public ProcessoController(IProcessoRepository repository, IbgeApiService ibgeApiService, ProcessoService processoService)
        {
            this.repository = repository;
            this.ibgeApiService = ibgeApiService;
            this.processoService = processoService;
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
                return Json(new { success = true, redirectTo = Url.Action(nameof(Index)) });
            }

            return Json(new { success = false, message = "Erro ao validar o formulário." });
        }

        [HttpGet]
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
            var result = await processoService.EditProcesso(id, processo);
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

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

        [HttpGet]
        public async Task<JsonResult> VerificarNpuExistente(string npu)
        {
            bool npuExistente = await repository.VerificarNpuExistenteAsync(npu);
            return Json(new { existe = npuExistente });
        }
    }
}
