using LibSql;

public class FleurCoSystem
{
    public Inventory Inventory { get; set; }
    public string ConfirmationMessages { get; set; }
    public SalesForecast SalesForecast { get; set; }
    public Order CurrentOrder { get; set; }
    private LibSqlConnection Connection { get; set; }
    public FleurCoSystem(LibSqlConnection connection)
    {
        Connection = connection;
    }
    public async Task DisplayInventory()
    {

        var inventory = await Inventory.DisplayInventory(Connection);
        foreach (var inventoryproduct in inventory)
        {
            Console.WriteLine(@$"-Inventory ID: {inventoryproduct.InventoryId}, Product ID: {inventoryproduct.ProductId} Product Name: {inventoryproduct.ProductName}, 
            Product Price: {inventoryproduct.ProductPrice:C},  Product Price: {inventoryproduct.ProductCost:C}, Product Category: {inventoryproduct.ProductCategory}");
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
    public async Task DisplayOrderList()
    {

        var orderList = await Order.DisplayOrderList(Connection);
        foreach (var order in orderList)
        {
            Console.WriteLine($"- Order ID: {order.OrderID}, Order Status: {order.OrderStatus} Order Type: {order.OrderType}, Order Total: {order.OrderTotal:C}");
        }

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