using api_tp.Models;

namespace api_tp.Servicios
{
    public interface IServicio
    {
        Task<List<Marcador>> obtenerMarcadores();
        
    }
}
