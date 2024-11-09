using LibSql;

public class FleurCoSystem
{
    public List<Order> Orders { get; set; } = [];
    public List<Invoice> Invoices { get; set; } = [];
    public Inventory Inventory { get; set; }
    public string ConfirmationMessages { get; set; }
    public SalesForecast SalesForecast { get; set; }
    public Order CurrentOrder { get; set; }
    private LibSqlConnection Connection { get; set; }
    private FleurCoSystem(LibSqlConnection connection, List<Order> orders, List<Invoice> invoices)
    {
        Connection = connection;
        Orders = orders;
        Invoices = invoices;
    }

    public static async Task<FleurCoSystem> CreateSystemAsync(LibSqlConnection connection)
    {
        var orderSql = "SELECT * FROM Orders";
        var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql);
        var orderCloseRequest = new LibSqlRequest(LibSqlOp.Close);
        var orders = await connection.Query<Order>(new List<LibSqlRequest> { orderDataRequest, orderCloseRequest });
        if (orders == null)
        {
            throw new InvalidOperationException("No Orders Found");
        }
        var invoiceSql = "SELECT * FROM Invoices";
        var invoiceDataRequest = new LibSqlRequest(LibSqlOp.Execute, invoiceSql);
        var invoiceCloseRequest = new LibSqlRequest(LibSqlOp.Close);
        var invoices = await connection.Query<Invoice>(new List<LibSqlRequest> { invoiceDataRequest, invoiceCloseRequest });
        if (invoices == null)
        {
            throw new InvalidOperationException("No Invoices Found");
        }
        var system = new FleurCoSystem(connection, orders.ToList(), invoices.ToList());
        return system;
    }

    public void DisplayInventory()
    {
        foreach (Product product in Inventory.Products)
        {
            Console.WriteLine($"ID:{product.ProductId}, Name: {product.ProductName}, Quantity: {product.GetProductQty(Inventory.Products)}, Price: {product.ProductPrice}, Cost: {product.ProductCost} Category: {product.ProductCategory}");
        }
    }
    public void SearchProduct()
    {

    }
    public void SelectProduct()
    {

    }

    public Product CreateNewProduct()
    {

        Console.WriteLine("Enter Product ID: ");
        int productId = int.Parse(Console.ReadLine());

        Console.Write("Enter Product Name: ");
        string productName = Console.ReadLine();

        Console.WriteLine("Enter Product Price: ");
        double productPrice = double.Parse(Console.ReadLine());

        Console.Write("Enter Product Cost: ");
        double productCost = double.Parse(Console.ReadLine());

        Console.Write("Enter Product Category: ");
        string productCategory = Console.ReadLine();

        var newProduct = new Product { ProductId = productId, ProductName = productName, ProductPrice = productPrice, ProductCost = productCost, ProductCategory = productCategory };

        Console.WriteLine("Product Created: ");
        Console.WriteLine(newProduct);
        return newProduct;
    }

    public void ConfirmAdd()
    {

    }
    public void ModifyProduct()
    {

    }
    public void ConfirmUpdate()
    {

    }
    public void RemoveProduct()
    {

    }
    public void ConfirmRemoval()
    {

    }
    public void RequestToGenerateForecast()
    {

    }
    public void SelectForecastCriteria()
    {

    }
    public void DisplayOrderList()
    {

    }
    public void SelectOrder()
    {

    }
    public void DisplayCustomerOrder()
    {

    }
    public void DisplayBackOrder()
    {

    }
    public void SelectProductToScan()
    {

    }
    public void ScanItemCode()
    {

    }
    public void PrintInvoice()
    {

    }
    public void AskConfirmation()
    {

    }
    public void ConfirmPrint()
    {

    }
    public void CreateBackOrder()
    {

    }
    public void EnterProductQty()
    {

    }
    public void ConfirmBackOrder()
    {

    }
    public void SelectFleurCoSystem()
    {

    }
    public void SelectFilter()
    {

    }
    public void ProvideItemCode()
    {

    }
}