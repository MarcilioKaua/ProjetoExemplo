using ProjetoExemplo.Models;
using ProjetoExemplo.Repositories;

namespace ProjetoExemplo.Services
{
    public class ProcessoService
    {
        private readonly IProcessoRepository repository;

        public ProcessoService(IProcessoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> EditProcesso(int id, Processo processo)
        {
            if (id != processo.Id)
            {
                return (false, "ID do processo não corresponde.");
            }

            var existingProcess = await repository.GetByIdAsync(id);
            if (existingProcess == null)
            {
                return (false, "O processo com o ID especificado não foi encontrado.");
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
            return (true, null);
        }
    }
}
