using Microsoft.EntityFrameworkCore;
using ProductsAssignment.Model;
using ProductsAssignment.Services;
using SimpleAuthentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<ProductContext>(opt =>
    opt.UseInMemoryDatabase("Products"));

builder.Services.AddSimpleAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSimpleAuthentication(builder.Configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use IServiceScope to access the scoped service provider.
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<ProductContext>();
    DataGenerator dataGenerator = new DataGenerator();
    // Call the DataGenerator to create sample data
    dataGenerator.Initialize(context);
}

app.UseHttpsRedirection();
app.UseAuthenticationAndAuthorization();
app.MapControllers();
app.Run();
