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
            Console.WriteLine
            (@$"Inventory ID: {inventoryproduct.InventoryId}, 
            Product ID: {inventoryproduct.ProductId} 
            Product Name: {inventoryproduct.ProductName}, 
            Product Price: {inventoryproduct.ProductPrice:C},  
            Product Cost: {inventoryproduct.ProductCost:C}, 
            Product Category: {inventoryproduct.ProductCategory}");
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
        string productName;
        do
        {
            Console.Write("Enter Product Name: ");
            productName = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrEmpty(productName))
            {
                Logger.Error("Product Name cannot be empty. Please enter a valid name.");
            }
        } while (string.IsNullOrEmpty(productName));

        double productPrice;
        do
        {
            Console.Write("Enter Product Price: ");
            string priceInput = Console.ReadLine() ?? string.Empty;
            if (!double.TryParse(priceInput, out productPrice))
            {
                Logger.Error("Please enter a valid price");
            }
        } while (productPrice <= 0);
        double productCost;
        do
        {
            Console.Write("Enter Product Cost: ");
            string costInput = Console.ReadLine() ?? string.Empty;
            if (!double.TryParse(costInput, out productCost))
            {
                Logger.Error("Please enter a valid cost");
            }
        } while (productCost <= 0);

        string productCategory;
        do
        {
            Console.Write("Enter Product Category: ");
            productCategory = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrEmpty(productCategory))
            {
                Logger.Error("Product Category cannot be empty. Please enter a valid category.");
            }
        } while (string.IsNullOrEmpty(productCategory));

        var newProduct = new Product { ProductId = Guid.NewGuid().ToString(), ProductName = productName, ProductPrice = productPrice, ProductCost = productCost, ProductCategory = productCategory };

        Console.WriteLine("\nProduct Details: \n");
        newProduct.DisplayProductInfo();
        return newProduct;
    }

    public async Task<Product?> ConfirmAdd(Product newProduct)
    {
        string userConfirmation;

        while (true)
        {

            Console.Write($"\nWould you like to save this product? Press [Y] for Yes, Press [N] for No: ");
            userConfirmation = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();


            switch (userConfirmation)
            {
                case "Y":
                case "YES":
                    await Product.ConfirmAdd(Connection, newProduct);
                    Logger.Success("New product was successfully added");

                    return newProduct;



                case "N":
                case "NO":
                    {
                        Logger.Warning("New product creation cancelled");

                        return null;
                    }
                default:

                    Logger.Error("Invalid input. Please enter [Y] or [N].");
                    break;


            }

        }
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
            Console.WriteLine
            (@$"- Order ID: {order.OrderID}, 
            Order Type: {order.OrderType}, 
            Order Status: {order.OrderStatus}, 
            Order Total: {order.OrderTotal:C}");
        }

    }

    public void SelectOrder()
    {

    }
    public async Task DisplayCustomerOrder()
    {
        var customerOrderList = await Order.DisplayCustomerOrders(Connection);
        foreach (var order in customerOrderList)
        {
            Console.WriteLine
            (@$"- Order ID: {order.OrderID}, 
            Order Type: {order.OrderType}, 
            Order Status: {order.OrderStatus}, 
            Order Total: {order.OrderTotal:C}");
        }


    }
    public async Task DisplayBackOrder()
    {
        var backOrderList = await Order.DisplayBackOrders(Connection);
        foreach (var order in backOrderList)
        {
            Console.WriteLine
            (@$"- Order ID: {order.OrderID}, 
            Order Type: {order.OrderType}, 
            Order Status: {order.OrderStatus},
            Order Total: {order.OrderTotal:C}");
        }


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