using Core.Entities;
using Infraestructure.Data;
using Infraestructure.Repositories;

namespace Core.Interfaces;

public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(TiendaContext context) : base(context)
    {
    }
}
