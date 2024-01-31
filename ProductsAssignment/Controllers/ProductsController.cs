using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAssignment.Services;
using SimpleAuthentication.JwtBearer;

namespace ProductsAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("Auth")]
        public IActionResult Login([FromServices] IJwtBearerService jwtBearerService)
        {
            try
            {
                var token = jwtBearerService.CreateToken("macro");
                return Ok(new LoginResponse(token));
            }
            catch (Exception ex)
            {
                LogAndBadRequest("Authentication", ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("healthCheck")]
        public IActionResult GetHealthCheck()
        {
            try
            {
                return Ok("OK");
            }
            catch (Exception ex)
            {
                LogAndBadRequest("HealthCheck", ex);
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                LogAndBadRequest("Get All Products", ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("{color}")]
        [Authorize]
        public IActionResult GetProductsByColor(string color)
        {
            try
            {
                var listOfColorProducts = _productService.GetProductsByColor(color);
                return Ok(listOfColorProducts);
            }
            catch (Exception ex)
            {
                LogAndBadRequest($"Get Products by Color ({color})", ex);
                return BadRequest(ex);
            }
        }

        private void LogAndBadRequest(string operation, Exception ex)
        {
            _logger.LogInformation($"{operation} error: {ex.Message}");
            _logger.LogError(ex, ex.Message);
            BadRequest(ex.Message);
        }

        public record LoginResponse(string Token);
    }
}
