using System.Text.Json.Serialization;

namespace ProjetoExemplo.DTOs
{
    public class IbgeMunicipioDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("microrregiao")]
        public Microrregiao Microrregiao { get; set; }

        [JsonPropertyName("regiao-imediata")]
        public RegiaoImediata RegiaoImediata { get; set; }
    }

    public class Microrregiao
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("mesorregiao")]
        public Mesorregiao Mesorregiao { get; set; }
    }

    public class Mesorregiao
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("UF")]
        public Uf Uf { get; set; }
    }

    public class Uf
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("regiao")]
        public Regiao Regiao { get; set; }
    }

    public class Regiao
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }

    public class RegiaoImediata
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("regiao-intermediaria")]
        public RegiaoIntermediaria RegiaoIntermediaria { get; set; }
    }

    public class RegiaoIntermediaria
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }
}
