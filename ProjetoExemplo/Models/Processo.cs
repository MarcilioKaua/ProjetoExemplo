namespace ProjetoExemplo.Models
{
    public class Processo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Npu { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataVisualizacao {  get; set; }
        public string Uf {  get; set; }
        public int MunicipioId { get; set; }
        public string MunicipioNome { get; set; }
    }
}
