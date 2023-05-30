namespace Core.Entities
{
    public class Categoria : BaseEntity
    {
        public string Nombre { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
