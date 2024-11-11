using LibSql;
public class Product
{
    [ColumnName("product_id")] public string ProductId { get; set; }
    [ColumnName("product_name")] public string ProductName { get; set; }
    [ColumnName("product_price")] public double ProductPrice { get; set; }
    [ColumnName("product_cost")] public double ProductCost { get; set; }
    [ColumnName("product_category")] public string ProductCategory { get; set; }
    // public Product(int productId, string productName, double productPrice, double productCost, string productCategory)
    // {
    //     ProductId = productId;
    //     ProductName = productName;
    //     ProductPrice = roductPrice;
    //     ProductCost = productCost;
    //     ProductCategory = productCategory;
    // }
    public void SearchProduct()
    {

    }
    public void SelectProduct()
    {

    }
    public static async Task ConfirmAdd(LibSqlConnection connection, Product newProduct)
    {
        var addSql = @"INSERT INTO Products (product_id, product_name, product_price, product_cost, product_category) VALUES (?,?,?,?,?) ";

        var addArgs = new List<LibSqlArg>
        {
            new LibSqlArg(newProduct.ProductId),
            new LibSqlArg(newProduct.ProductName),
            new LibSqlArg(newProduct.ProductPrice),
            new LibSqlArg(newProduct.ProductCost),
            new LibSqlArg(newProduct.ProductCategory)

        };
        var addDataRequest = new LibSqlRequest(LibSqlOp.Execute, addSql, addArgs);
        await connection.Execute([addDataRequest]);
    }
    public void ConfirmUpdate()
    {

    }
    public void ConfirmRemoval()
    {

    }
    public int GetProductQty(List<Product> products)
    {
        List<Product> foundProducts = products.FindAll(p => p.ProductId == ProductId);
        return foundProducts.Count;
    }
    public void EnterProductQty()
    {

    }
    public void DisplayProductCategory()
    {

    }
    public void DisplayProductInfo()
    {
        Console.WriteLine($"Product Name: {ProductName}, Product Price: {ProductPrice}, Product Cost: {ProductCost}, Product Category: {ProductCategory},");
    }
}
