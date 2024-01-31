using System.Collections.Generic;

namespace ProductsAssignment.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<Product> GetProductsByColor(string color);
    }
}
