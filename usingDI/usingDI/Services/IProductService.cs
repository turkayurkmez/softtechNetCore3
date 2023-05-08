using usingDI.Models;

namespace usingDI.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }

    public class AProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            return new()
            {
                new (){ Id=1, Name="A", Price=100 },
                new (){ Id=2, Name="B", Price=101 },
                new (){ Id=3, Name="C", Price=102 },

            };
        }
    }

    public class BProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            return new()
            {
                new (){ Id=1, Name="X", Price=100 },
                new (){ Id=2, Name="Y", Price=201 },
                new (){ Id=3, Name="Z", Price=302 },

            };
        }
    }
}
