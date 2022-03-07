using Newtonsoft.Json;

namespace api_test.Entities
{
    public class BaseResponse
    {
        [JsonProperty("cantidadV")]
        public int cantidadV { get; set; }

        [JsonProperty("cantidadM")]
        public int cantidadM { get; set; }

    }

    public class ResponseIntegration<T> : BaseResponse
    {
        [JsonProperty("listaVivos")]
        public T listaVivos { get; set; }
        [JsonProperty("listaMuertos")]
        public T listaMuertos { get; set; }
    }
}
