# Dapper setup
### Step 1: Install Dapper

First, you'll need to add the Dapper NuGet package to your project. You can do this using the NuGet Package Manager in Visual Studio or via the command line.

#### Using the NuGet Package Manager:
1. Right-click on your project in the Solution Explorer and select "Manage NuGet Packages".
2. Search for "Dapper" and click "Install".

#### Using the command line:
Open the Package Manager Console and run the following command:
```sh
Install-Package Dapper
```

### Step 2: Setup Your Database Connection

You'll need a connection string to connect to your database. Add your connection string to the `appsettings.json` file.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
  }
}
```

### Step 3: Create a Database Context

Create a class to manage your database connection. This class will be responsible for providing the connection to your database.

```csharp
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
```

### Step 4: Register DapperContext in the Dependency Injection Container

Modify the `Program.cs` or `Startup.cs` (depending on your project setup) to register the `DapperContext` so it can be injected into your controllers.

In `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();

var app = builder.Build();
```

### Step 5: Create a Repository

Create a repository class to handle your data access logic. This is where you will use Dapper to execute SQL queries.

```csharp
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductRepository
{
    private readonly DapperContext _context;

    public ProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        var query = "SELECT * FROM Products";

        using (var connection = _context.CreateConnection())
        {
            var products = await connection.QueryAsync<Product>(query);
            return products;
        }
    }

    public async Task<Product> GetProductById(int id)
    {
        var query = "SELECT * FROM Products WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var product = await connection.QuerySingleOrDefaultAsync<Product>(query, new { Id = id });
            return product;
        }
    }

    public async Task CreateProduct(Product product)
    {
        var query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { product.Name, product.Price });
        }
    }

    public async Task UpdateProduct(Product product)
    {
        var query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { product.Name, product.Price, product.Id });
        }
    }

    public async Task DeleteProduct(int id)
    {
        var query = "DELETE FROM Products WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
```

### Step 6: Create a Controller

Create a controller to expose the endpoints for your API. Inject the `ProductRepository` and use it to handle requests.

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _repository;

    public ProductsController(ProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _repository.GetProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _repository.GetProductById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        await _repository.CreateProduct(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        await _repository.UpdateProduct(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _repository.DeleteProduct(id);
        return NoContent();
    }
}
```

### Step 7: Define Your Product Model

Create a `Product` class that represents the data structure.

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

### Step 8: Run Your Application

Make sure everything is set up correctly, and run your application. You should be able to use the endpoints to perform CRUD operations on the `Products` table in your database.

---

This guide covers the basic setup and implementation of Dapper in a .NET 8 Web API project. Let me know if you have any questions or need further assistance with specific parts of the process.