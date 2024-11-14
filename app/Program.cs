using LibSql;

class Program
{
    static readonly Menu MenuOption = new(["1. Inventory", "2. Orders", "3. Sales Forecasts", "4. Exit"]);
    static readonly Menu InventoryOption = new(["1. Display Inventory", "2. Update Inventory", "3. Add New Product", "4. Modify Existing Product", "5. Remove Product", "6. Go Back", "7. Exit"]);
    static readonly Menu OrderOption = new(["1. Display Order List", "2. Customer Orders", "3. Backorders", "4. Create Backorder", "5. Go Back", "6. Exit"]);
    static readonly Menu ForecastOption = new(["1. Create Forecast", "2. Display Past Forecasts", "3. Go Back", "4. Exit"]);
    static async Task Main(string[] args)
    {



        DotNetEnv.Env.Load();
        string? tursoDb = Environment.GetEnvironmentVariable("TURSO_DB");
        string? tursoOrg = Environment.GetEnvironmentVariable("TURSO_ORG");
        string? tursoToken = Environment.GetEnvironmentVariable("TURSO_TOKEN");

        if (tursoDb == null)
        {
            Console.Error.WriteLine("Missing TURSO_DB env var");
            return;
        }
        if (tursoOrg == null)
        {
            Console.Error.WriteLine("Missing TURSO_ORG env var");
            return;
        }
        if (tursoToken == null)
        {
            Console.Error.WriteLine("Missing TURSO_TOKEN env var");
            return;
        }

        var connection = new LibSqlConnection(tursoOrg, tursoDb, tursoToken);
        var system = new FleurCoSystem(connection);

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("FleurCo Inventory System\n");
            MenuOption.ShowOptions();
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("\nYou selected Inventory\n");
                    InventoryOption.ShowOptions();
                    Console.WriteLine("\nSelect an option: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("\nDisplaying Inventory");
                            await system.DisplayInventory();
                            break;
                        case "2":
                            Console.WriteLine("\nYou Selected Update Inventory");
                            var inventoryQty = await system.DisplayInventory();
                            var itemToUpdate = system.SelectInventoryItem(inventoryQty.ToList());
                            double updatedQty = system.EnterQty(itemToUpdate);
                            await system.ConfirmUpdateQty(itemToUpdate, updatedQty);
                            break;
                        case "3":
                            Console.WriteLine("\nYou Selected Add New Product");

                            var newProduct = system.CreateNewProduct();
                            await system.ConfirmAdd(newProduct);
                            break;
                        case "4":
                            Console.WriteLine("\nYou Selected Modify Existing Product");
                            var productLineM = await system.DisplayProductLine();
                            var productToModify = system.SelectProduct(productLineM.ToList());
                            system.ModifyProduct(productToModify);
                            await system.ConfirmModification(productToModify);
                            break;
                        case "5":
                            Console.WriteLine("\nYou Selected Remove Product");
                            var productLineR = await system.DisplayProductLine();
                            var productToRemove = system.SelectProduct(productLineR.ToList());
                            var removedProduct = system.RemoveProduct(productToRemove);
                            await system.ConfirmRemoval(removedProduct);
                            break;
                        case "6":
                            Console.WriteLine("\nReturning to Previous Menu...");
                            break;
                        case "7":
                            Console.WriteLine("\nExiting...");
                            exit = true;
                            break;
                    }

                    break;
                case "2":
                    Console.WriteLine("\nYou selected Orders\n");
                    OrderOption.ShowOptions();
                    Console.WriteLine("\nSelect an option: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("\nDisplaying Order List:");
                            var orderList = await system.DisplayOrderList();
                            system.SelectOrder(orderList.ToList());
                            break;
                        case "2":
                            Console.WriteLine("\nYou Selected Customer Orders");
                            var customerOrderList = await system.DisplayCustomerOrders();
                            system.SelectOrder(customerOrderList.ToList());
                            break;
                        case "3":
                            Console.WriteLine("\nYou Selected Backorders");
                            var backOrderList = await system.DisplayBackOrders();
                            system.SelectOrder(backOrderList.ToList());
                            break;
                        case "4":
                            Console.WriteLine("\nCreating Backorder");
                            await system.CreateBackOrder();
                            break;
                        case "5":
                            Console.WriteLine("\nReturning to Previous Menu...");
                            break;
                        case "6":
                            Console.WriteLine("\nExiting...");
                            exit = true;
                            break;
                    }
                    break;
                case "3":
                    Console.WriteLine("\nYou selected Sales Forecasts\n");
                    ForecastOption.ShowOptions();
                    Console.WriteLine("\nSelect an option: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("\nYou Selected Create Forecast");
                            break;
                        case "2":
                            Console.WriteLine("\nDisplaying Past Forecasts");
                            break;
                        case "3":
                            Console.WriteLine("\nReturning to Previous Menu...");
                            break;
                        case "4":
                            Console.WriteLine("\nExiting...");
                            exit = true;
                            break;
                    }
                    break;
                case "4":
                    Console.WriteLine("\nExiting...");
                    exit = true;
                    break;
                default:
                    Logger.Error("Invalid option. Please try again.");
                    break;
            }

            if (!exit)
            {
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
        }
        await connection.Close();
    }
}