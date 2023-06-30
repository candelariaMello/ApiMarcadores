using Microsoft.AspNetCore.Http;
using System.Net;

namespace api_tp.Models
{
    public class ResultadoMarcadores
    {
        public string Error { get; set; }
        public bool Ok { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<Marcador> LitadoMarcadores { get; set; }

    }
}
