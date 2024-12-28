using ProjetoExemplo.DTOs;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ProjetoExemplo.Services
{
    public class IbgeApiService
    {
        private readonly HttpClient _httpClient;

        public IbgeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IbgeMunicipioDto>> GetMunicipiosByUf(string uf)
        {
            return await _httpClient.GetFromJsonAsync<List<IbgeMunicipioDto>>($"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios");
        }
    }
}
