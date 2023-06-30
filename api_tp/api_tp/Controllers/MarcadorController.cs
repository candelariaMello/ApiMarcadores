using Microsoft.AspNetCore.Mvc;
using api_tp.Models;
using api_tp.Servicios;
using System.Text.Json;

namespace api_tp.Controllers
{
    [ApiController]
    public class MarcadorController : ControllerBase
    {
        private readonly IServicio _servicio;

        public MarcadorController(IServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        [Route("/api/marcadores/getMarcadores")]
        public async Task<List<Marcador>> getMarcadores()
        {
            return await _servicio.obtenerMarcadores();
        }

    }
}