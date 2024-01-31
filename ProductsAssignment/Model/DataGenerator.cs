namespace ProductsAssignment.Model
{
    public class DataGenerator
    {
        private readonly ProductContext _context;

        public DataGenerator()
        {
        }


        public DataGenerator(ProductContext context)
        {
            _context = context;
        }

        public void Initialize(ProductContext _context)
        {
            // Check if there are any records in the Products of the context
            if (_context.Products.Any())
            {
                return;  
            }
            
            _context.Products.AddRange(

                new Product { ProductId = 1, Model = "Smartphone A", Brand = "BrandA", Price = 799.99m, Color = "Black", OperatingSystem = "Android" },
                new Product { ProductId = 2, Model = "Smartphone B", Brand = "BrandB", Price = 699.99m, Color = "White", OperatingSystem = "iOS" },
                new Product { ProductId = 3, Model = "Smartphone C", Brand = "BrandC", Price = 299.99m, Color = "Blue", OperatingSystem = "Android" },
                new Product { ProductId = 4, Model = "Smartphone D", Brand = "BrandD", Price = 1299.99m, Color = "Silver", OperatingSystem = "iOS" },
                new Product { ProductId = 5, Model = "Smartphone E", Brand = "BrandE", Price = 499.99m, Color = "Gold", OperatingSystem = "Android" });

            _context.SaveChanges();
        }
    }
}
