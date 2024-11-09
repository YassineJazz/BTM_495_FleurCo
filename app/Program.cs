class Program
{
    static readonly Menu MenuOption = new(["1. Inventory", "2. Orders", "3. Sales Forecasts", "4. Exit"]);
    static readonly Menu InventoryOption = new(["1. Display Inventory", "2. Add New Product", "3. Modify Existing Product", "4. Remove Product", "5. Exit"]);

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("FleurCo Inventory System");
            MenuOption.ShowOptions();
            Console.Write("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("You selected Inventory");
                    InventoryOption.ShowOptions();
                    break;
                case "2":
                    Console.WriteLine("You selected Orders");
                    break;
                case "3":
                    Console.WriteLine("You selected Sales Forecasts");
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            if (!exit)
            {
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }
}