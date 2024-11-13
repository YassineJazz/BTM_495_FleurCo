using LibSql;
public class FleurCoSystem
{
    public Inventory Inventory { get; set; }
    public string ConfirmationMessages { get; set; }
    public SalesForecast SalesForecast { get; set; }
    public Order CurrentOrder { get; set; }
    static readonly Menu CreateBackOrderOption = new(["1. Add Item to Backorder", "2. Display Current Backorder", "3. Complete Backorder", "4. Cancel Backorder"]);
    private LibSqlConnection Connection { get; set; }
    public FleurCoSystem(LibSqlConnection connection)
    {
        Connection = connection;
    }
    public async Task<Rows<InventoryProduct>> DisplayInventory()
    {
        var inventory = await Inventory.DisplayInventory(Connection);
        int index = 1;
        foreach (var inventoryproduct in inventory)
        {
            Console.WriteLine
            (@$"{index}. Product Name: {inventoryproduct.ProductName}, 
            Product Price: {inventoryproduct.ProductPrice:C},  
            Product Cost: {inventoryproduct.ProductCost:C}, 
            Product Category: {inventoryproduct.ProductCategory},
            Quantity: {inventoryproduct.Quantity}");

            index++;
        }
        return inventory;
    }
    public async Task<Rows<Product>> DisplayProductLine()
    {
        var products = await Product.DisplayProductLine(Connection);
        int index = 1;
        foreach (var product in products)
        {
            Console.WriteLine
            (@$"{index}. Product Name: {product.ProductName}, 
            Product Price: {product.ProductPrice:C},  
            Product Cost: {product.ProductCost:C}, 
            Product Category: {product.ProductCategory}");
            index++;
        }
        return products;
    }

    public void SearchProduct()
    {

    }
    public Product SelectProduct(List<Product> products)
    {
        int chosenIndex;
        do
        {
            Console.Write("Enter the index of the product to select: ");
            string indexInput = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(indexInput, out chosenIndex) && chosenIndex > 0 && chosenIndex <= products.Count)
            {
                chosenIndex--;
                break;

            }
            Logger.Error("Please enter a valid product index: ");
        } while (true);
        var selectedProduct = products[chosenIndex];

        return selectedProduct;
    }
    public InventoryProduct SelectInventoryItem(List<InventoryProduct> items)
    {
        int chosenIndex;
        do
        {
            Console.Write("Enter the index of the item to select: ");
            string indexInput = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(indexInput, out chosenIndex) && chosenIndex > 0 && chosenIndex <= items.Count)
            {
                chosenIndex--;
                break;

            }
            Logger.Error("Please enter a valid item index: ");
        } while (true);
        var selectedItem = items[chosenIndex];

        return selectedItem;
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

    public async Task ConfirmAdd(Product newProduct)
    {
        string userConfirmation;
        var running = true;
        while (running)
        {

            Console.Write($"\nWould you like to save this product? Press [Y] for Yes, Press [N] for No: ");
            userConfirmation = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();


            switch (userConfirmation)
            {
                case "Y":
                case "YES":
                    await Product.ConfirmAdd(Connection, newProduct);
                    await Inventory.AddProduct(Connection, newProduct);
                    Logger.Success("New product was successfully added");
                    running = false;
                    break;



                case "N":
                case "NO":
                    {
                        Logger.Warning("New product creation cancelled");
                        running = false;
                        break;
                    }
                default:

                    Logger.Error("Invalid input. Please enter [Y] or [N].");
                    break;


            }


        }

    }

    public double updateQty(InventoryProduct itemToUpdate)
    {
        itemToUpdate.DisplayItemInfo();
        double newQuanity;
        do
        {
            Console.Write("Enter New Quantity: ");
            var qtyInput = Console.ReadLine() ?? string.Empty;
            if (!double.TryParse(qtyInput, out newQuanity))
            {
                Logger.Error("Please enter a valid quantity");
            }
        } while (newQuanity < 0);
        return newQuanity;
    }

    public async Task<Product?> ConfirmUpdateQty(InventoryProduct itemToUpdate, double newQuantity)
    {
        string userConfirmation;

        while (true)
        {

            Console.Write($"\nSave Changes? \nPress [Y] for Yes, Press [N] for No: ");
            userConfirmation = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();


            switch (userConfirmation)
            {
                case "Y":
                case "YES":
                    await Inventory.ConfirmUpdateQty(Connection, itemToUpdate, newQuantity);
                    Logger.Success("Quantity was successfully updated");
                    return itemToUpdate;



                case "N":
                case "NO":
                    {
                        Logger.Warning("Quantity Changes were cancelled");

                        return null;
                    }
                default:

                    Logger.Error("Invalid input. Please enter [Y] or [N].");
                    break;


            }

        }
    }
    public Product ModifyProduct(Product productToModify)
    {
        string productName;
        productToModify.DisplayProductInfo();
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

        var modifiedProduct = new Product { ProductId = productToModify.ProductId, ProductName = productName, ProductPrice = productPrice, ProductCost = productCost, ProductCategory = productCategory };
        Console.WriteLine("\nProduct Details: \n");
        modifiedProduct.DisplayProductInfo();
        return modifiedProduct;
    }

    public async Task ConfirmModification(Product modifiedProduct)
    {
        string userConfirmation;
        var running = true;
        while (running)
        {

            Console.Write($"\nAre you sure you want to modify this product? \nPress [Y] for Yes, Press [N] for No: ");
            userConfirmation = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();


            switch (userConfirmation)
            {
                case "Y":
                case "YES":
                    await Product.ConfirmUpdate(Connection, modifiedProduct);
                    Logger.Success("Product was successfully modified");
                    running = false;
                    break;



                case "N":
                case "NO":
                    {
                        Logger.Warning("Product modification was cancelled");

                        running = false;
                        break;
                    }
                default:

                    Logger.Error("Invalid input. Please enter [Y] or [N].");
                    break;


            }

        }
    }
    public Product RemoveProduct(Product productToRemove)
    {

        Console.WriteLine("\nProduct Details: \n");
        productToRemove.DisplayProductInfo();
        return productToRemove;

    }
    public async Task ConfirmRemoval(Product productToRemove)
    {
        string userConfirmation;

        var running = true;
        while (running)
        {

            Console.Write($"\nAre you sure you want to remove this product? \nPress [Y] for Yes, Press [N] for No: ");
            userConfirmation = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();


            switch (userConfirmation)
            {
                case "Y":
                case "YES":
                    await Product.ConfirmRemoval(Connection, productToRemove);
                    Logger.Success("Product was successfully removed");

                    running = false;
                    break;



                case "N":
                case "NO":
                    {
                        Logger.Warning("Product removal was cancelled");

                         running = false;
                         break;
                    }
                default:

                    Logger.Error("Invalid input. Please enter [Y] or [N].");
                    break;


            }

        }
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
    private List<InventoryProduct> newBackOrder = new List<InventoryProduct>();
    public async Task CreateBackOrder()
    {
        var running = true;
        while (running)
        {
            CreateBackOrderOption.ShowOptions();
            Console.Write("\nSelect Backorder Option: ");
            switch (Console.ReadLine())
            {
                case "1":
                    var backOrderInventory = await DisplayInventory();
                    var itemForBackorder = SelectInventoryItem(backOrderInventory.ToList());
                    newBackOrder = AddToBackOrder(itemForBackorder);
                    Logger.Success("Item Added To Backorder");
                    break;
                case "2":
                    foreach (var item in newBackOrder)
                    {
                        Console.WriteLine($"Product Name: {item.ProductName}, Product Price: {item.ProductPrice}, Product Cost: {item.ProductCost}, Product Category: {item.ProductCategory},");
                    }
                    break;

                case "3":

                    var confirmedBackOrder = await ConfirmBackOrder(newBackOrder);
                    running = false;
                    break;
                case "4":
                    Logger.Warning("Backorder Cancelled");
                    running = false;
                    break;
                default:
                    Logger.Error("Invalid Option, Please Try Again");
                    break;
            }
        }
    }
    public List<InventoryProduct> AddToBackOrder(InventoryProduct item)
    {
        newBackOrder.Add(item);
        return newBackOrder;
    }
    public void EnterProductQty()
    {

    }
    public async Task<List<InventoryProduct>?> ConfirmBackOrder(List<InventoryProduct> newBackOrder)
    {
        string userConfirmation;

        var running = true;
        while (running)
        {

            Console.Write($"\nDo you want to save this backorder? \nPress [Y] for Yes, Press [N] for No: ");
            userConfirmation = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();


            switch (userConfirmation)
            {
                case "Y":
                case "YES":
                    await BackOrder.ConfirmBackOrder(Connection, newBackOrder);
                    Logger.Success("Backorder was successfully created");
                    running = false;
                    return newBackOrder;



                case "N":
                case "NO":
                    {
                        Logger.Warning("Backorder creation cancelled");
                        running = false;
                        return null;
                    }
                default:

                    Logger.Error("Invalid input. Please enter [Y] or [N].");
                    break;


            }

        }
        return null;
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