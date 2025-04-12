using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRelevanciaService
    {
        double Calcular(Repositorio repo);
    }
}
