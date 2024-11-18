using LibSql;
using FleurCo_API.Classes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<FleurCoSystem>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    Console.WriteLine("Initializing LibSqlConnection");
    var org = configuration["Turso:Org"] ?? Environment.GetEnvironmentVariable("TURSO_ORG");
    if (string.IsNullOrEmpty(org))
    {
        throw new Exception("TURSO_ORG is not set");
    }
    var db = configuration["Turso:Db"] ?? Environment.GetEnvironmentVariable("TURSO_DB");
    if (string.IsNullOrEmpty(db))
    {
        throw new Exception("TURSO_DB is not set");
    }
    var token = configuration["Turso:Token"] ?? Environment.GetEnvironmentVariable("TURSO_TOKEN");
    if (string.IsNullOrEmpty(token))
    {
        throw new Exception("TURSO_TOKEN is not set");
    }
    var conn = new LibSqlConnection(org, db, token);
    return new FleurCoSystem(conn);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () =>
{
    return "Welcome To FleurCo Inventory System!";
});


//inventory
app.MapGet("/inventory", async (FleurCoSystem system) =>
{
    return await system.DisplayInventory();
});
app.MapGet("/inventory/{id}", async (string id, FleurCoSystem system) =>
{
    var result = await system.GetItem(id);
    if (result.Count() != 1)
    {
        return Results.NotFound("Item Not Found");
    }

    return Results.Ok(result.First());
});
app.MapPatch("/inventory/{id}", async (string id, InventoryPatchRequest request, FleurCoSystem system) =>
{
    await system.UpdateQty(id, request.Quantity);
    var updatedItem = await system.GetItem(id);
    return Results.Ok(updatedItem);
});


//products
app.MapGet("/products", async (FleurCoSystem system) =>
{
    return await system.DisplayProductLine();
});
app.MapGet("/products/{id}", async (string id, FleurCoSystem system) =>
{
    var result = await system.GetProduct(id);
    if (result.Count() != 1)
    {
        return Results.NotFound("Product Not Found");
    }
    return Results.Ok(result.First());
}
);
app.MapDelete("/products/{id}", async (string id, FleurCoSystem system) =>
{
    await system.DeleteProduct(id);
    return Results.Ok();
});
app.MapPost("/products", async (ProductPostRequest request, FleurCoSystem system) =>
{
    var newProduct = await system.CreateNewProduct(request.ProductName, request.ProductPrice, request.ProductCost, request.ProductCategory);
    return Results.Created($"/products/{newProduct.ProductId}", newProduct);
});
app.MapPut("/products/{id}", async (string id, ProductPutRequest request, FleurCoSystem system) =>
{
    var modifiedProduct = await system.ModifyProduct(id, request.ProductName, request.ProductPrice, request.ProductCost, request.ProductCategory);
    return Results.Ok(modifiedProduct);
});



//orders
app.MapGet("/orders", async (FleurCoSystem system) =>
{
    return await system.DisplayOrderList();
});
app.MapGet("/backorders", async (FleurCoSystem system) =>
{
    return await system.DisplayBackOrders();
});
app.MapPost("/backorders", async (List<BackOrderPostRequest> request, FleurCoSystem system) =>
{
    var newOrderGuid = await system.CreateBackorder(request);
    var newBackOrder = await system.GetOrder(newOrderGuid);
    return Results.Created($"/Backorder/{newOrderGuid}", newBackOrder);
});
app.MapGet("/customerorders", async (FleurCoSystem system) =>
{
    return await system.DisplayCustomerOrders();
});
app.MapGet("/orders/{id}", async (string id, FleurCoSystem system) =>
{
    var result = await system.GetOrder(id);
    if (result.Count() != 1)
    {
        return Results.NotFound("Order Not Found");
    }
    return Results.Ok(result.First());
}
);
app.MapGet("/orders/{id}/products/", async (string id, FleurCoSystem system) =>
{
    var result = await system.GetOrderProducts(id);
    return Results.Ok(result);
}
);



app.Run();

public class ProductPostRequest
{
    [JsonPropertyName("productName")]
    public required string ProductName { get; set; }
    [JsonPropertyName("productPrice")]
    public required double ProductPrice { get; set; }
    [JsonPropertyName("productCost")]
    public required double ProductCost { get; set; }
    [JsonPropertyName("productCategory")]
    public required string ProductCategory { get; set; }

}
public class ProductPutRequest
{
    [JsonPropertyName("productName")]
    public required string ProductName { get; set; }
    [JsonPropertyName("productPrice")]
    public required double ProductPrice { get; set; }
    [JsonPropertyName("productCost")]
    public required double ProductCost { get; set; }
    [JsonPropertyName("productCategory")]
    public required string ProductCategory { get; set; }

}

public class InventoryPatchRequest
{
    public required double Quantity { get; set; }
}
public class BackOrderPostRequest
{
    [JsonPropertyName("productId")]
    public required string ProductId { get; set; }
    [JsonPropertyName("productName")]
    public required string ProductName { get; set; }
    [JsonPropertyName("productPrice")]
    public required double ProductPrice { get; set; }
    [JsonPropertyName("productCost")]
    public required double ProductCost { get; set; }
    [JsonPropertyName("productCategory")]
    public required string ProductCategory { get; set; }
    [JsonPropertyName("quantity")]
    public required double Quantity { get; set; }
    [JsonPropertyName("inventoryId")]
    public required string InventoryId { get; set; }
}