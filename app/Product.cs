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
    public static async Task<Rows<Product>> DisplayProductLine(LibSqlConnection connection)
    {
        var productLineSql = "SELECT * FROM Products";
        var productLineDataRequest = new LibSqlRequest(LibSqlOp.Execute, productLineSql);
        var productLine = await connection.Query<Product>([productLineDataRequest]);
        if (productLine == null)
        {
            throw new InvalidOperationException("No Products To Display");
        }
        return productLine;
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
    public static async Task ConfirmUpdate(LibSqlConnection connection, Product modifiedProduct)

    {
        var updateSql = @"UPDATE Products SET product_name = ?, product_price = ?, product_cost = ?, product_category = ?, WHERE product_id = ?";

        var updateArgs = new List<LibSqlArg>
        {
            new LibSqlArg(modifiedProduct.ProductName),
            new LibSqlArg(modifiedProduct.ProductPrice),
            new LibSqlArg(modifiedProduct.ProductCost),
            new LibSqlArg(modifiedProduct.ProductCategory),
            new LibSqlArg(modifiedProduct.ProductId)

        };
        var addDataRequest = new LibSqlRequest(LibSqlOp.Execute, updateSql, updateArgs);
        await connection.Execute([addDataRequest]);
    }
    public static async Task ConfirmRemoval(LibSqlConnection connection, Product productToRemove)
    {
        var deleteSql = @"DELETE FROM Products WHERE product_id = ?";

        var deleteArgs = new List<LibSqlArg>
        {
            new LibSqlArg(productToRemove.ProductId)

        };
        var addDataRequest = new LibSqlRequest(LibSqlOp.Execute, deleteSql, deleteArgs);
        await connection.Execute([addDataRequest]);
    }
    public int GetProductQty(List<Product> products)
    {
        List<Product> foundProducts = products.FindAll(p => p.ProductId == ProductId);
        return foundProducts.Count;
    }
    public static double EnterQty()
    {
        double newQuantity;
        do
        {
            Console.Write("Enter quantity: ");
            string qtyInput = Console.ReadLine() ?? string.Empty;
            if (!double.TryParse(qtyInput, out newQuantity))
            {
                Logger.Error("Please enter a quantity");
            }
        } while (newQuantity <= 0);
        return newQuantity;
    }
    public void DisplayProductCategory()
    {

    }
    public void DisplayProductInfo()
    {
        Console.WriteLine($"Product Name: {ProductName}, Product Price: {ProductPrice}, Product Cost: {ProductCost}, Product Category: {ProductCategory},");
    }
}
