using ProductsAssignment.Model;

namespace ProductsAssignment.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;

        public ProductService(ProductContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetProductsByColor(string color)
        {
            return _context.Products.Where(x => x.Color.ToLower() == color.ToLower()).ToList();
        }
    }
}
