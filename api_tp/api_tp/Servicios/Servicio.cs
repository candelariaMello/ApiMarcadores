using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using api_tp.Models;
using ResultadoLogin = api_tp.Models.ResultadoLogin;

namespace api_tp.Servicios
{
    public class Servicio : IServicio
    {
        private static string _nombreUsuario;
        private static string _password;
        private static string _baseurl;
        private static string _token;

        public Servicio()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _nombreUsuario = builder.GetSection("ApiSettings:nombreUsuario").Value;
            _password = builder.GetSection("ApiSettings:password").Value;
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;

        }

        public async Task Autenticacion()
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var credenciales = new Usuario() { NombreUsuario = _nombreUsuario, Password = _password };

            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync("/api/usuario/LoginUsuarioWeb", content);
            var json_respuesta = await respuesta.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<ResultadoLogin>(json_respuesta);

            _token = resultado.Token;
        }

        public async Task<List<Marcador>> obtenerMarcadores()
        {
            List<Marcador> marcadores = new List<Marcador>();

            await Autenticacion();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/marcador/GetMarcadores");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoMarcadores>(json_respuesta);
                marcadores = resultado.LitadoMarcadores;
            }
            return marcadores;

        }
    }
}
