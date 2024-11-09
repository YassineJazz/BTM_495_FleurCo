using LibSql;

public class Inventory
{

    public List<Product> Products { get; set; } = [];
    private LibSqlConnection Connection { get; set; }
    private Inventory(LibSqlConnection connection, List<Product> products)
    {
        Connection = connection;
        Products = products;
    }

    public static async Task<Inventory> CreateInvAsynch(LibSqlConnection connection)
    {
        var productSql = "SELECT * FROM Products";
        var productDataRequest = new LibSqlRequest(LibSqlOp.Execute, productSql);
        var productCloseRequest = new LibSqlRequest(LibSqlOp.Close);

        var products = await connection.Query<Product>(new List<LibSqlRequest> { productDataRequest, productCloseRequest });
        if (products == null)
        {
            throw new InvalidOperationException("No Products Found");
        }
        var inventory = new Inventory(connection, products.ToList());
        return inventory;

    }
    public void DisplayInventory()
    {

    }
    public void AddProduct(Product product)
    {

        Products.Add(product);

    }
    public void UpdateProduct()
    {

    }
    public void DeleteProduct()
    {

    }
    public void IncreaseProductQty()
    {

    }
    public void DecreaseProdQty()
    {

    }
    public void RetrieveItemData()
    {

    }
}