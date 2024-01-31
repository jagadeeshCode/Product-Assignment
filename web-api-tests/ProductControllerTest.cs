using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsAssignment.Controllers;
using ProductsAssignment.Model;
using ProductsAssignment.Services;

namespace Products.Test.Integration
{
    public class ProductControllerTest
    {
        private readonly ProductsController _productsController;
        private readonly Mock<IProductService> _productServiceMock;

        public ProductControllerTest()
        {
            // Create a mock for IProductService
            _productServiceMock = new Mock<IProductService>();

            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Create the context using the in-memory database
            var context = new ProductContext(options);

            var productService = new ProductService(context);
            var loggerMock = new Mock<ILogger<ProductsController>>();

            // Inject the mock into the controller
            _productsController = new ProductsController(productService, loggerMock.Object);

            // Populate the in-memory database with sample data (if needed)
            DataGenerator dataGenerator = new DataGenerator();
            dataGenerator.Initialize(context);
        }

        [Fact]
        public void GetHealthCheck_ReturnsOkResultWithMessage()
        {
            var result = _productsController.GetHealthCheck() as OkObjectResult;

            Assert.NotNull(result);

            Assert.Equal("OK", result.Value);
        }

        [Fact]
        public void GetProducts_ReturnsOkResultWithAllProducts()
        {
            _productServiceMock.Setup(x => x.GetAllProducts()).Returns(new List<Product>());

            var result = _productsController.GetProducts();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetProducts_ReturnsAllProductsCount()
        {
            var expectedCount = 5;

            _productServiceMock.Setup(x => x.GetAllProducts()).Returns(new List<Product>());

            var result = _productsController.GetProducts() as OkObjectResult;

            Assert.NotNull(result);

            Assert.IsType<List<Product>>(result.Value);

            var items = Assert.IsType<List<Product>>(result.Value);

            Assert.Equal(expectedCount, items.Count);
        }

        [Theory]
        [InlineData("White")]
        [InlineData("Black")]
        [InlineData("Gold")]
        public void GetProductsByColor_ReturnsOkResultWithFilteredProducts(string color)
        {
            _productServiceMock.Setup(x => x.GetProductsByColor(color)).Returns(new List<Product>());

            var result = _productsController.GetProductsByColor(color) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("White")]
        [InlineData("Black")]
        [InlineData("Gold")]
        public void GetProductsByColor_ReturnsAtLeastOneProduct(string color)
        {
            _productServiceMock.Setup(x => x.GetProductsByColor(color)).Returns(new List<Product>());

            var result = _productsController.GetProductsByColor(color) as OkObjectResult;

            Assert.NotNull(result);

            Assert.IsType<List<Product>>(result.Value);

            var items = Assert.IsType<List<Product>>(result.Value);

            Assert.True(items.Count > 0, "Expected at least one product");
        }
    }
}
