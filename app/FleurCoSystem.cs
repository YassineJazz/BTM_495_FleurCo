public class FleurCoSystem
{
    public List<Order> Orders { get; set; }
    public List<Invoice> Invoices { get; set; }
    public Inventory Inventory { get; set; }
    public string ConfirmationMessages { get; set; }
    public SalesForecast SalesForecast { get; set; }
    public Order CurrentOrder { get; set; }

    public FleurCoSystem(List<Order> orders, List<Invoice> invoices, Inventory inventory, string confirmationmessages, SalesForecast salesforecast, Order currentorder)
    {
        Orders = orders;
        Invoices = invoices;
        Inventory = inventory;
        ConfirmationMessages = confirmationmessages;
        SalesForecast = salesforecast;
        CurrentOrder = currentorder;
    }
    public void DisplayInventory()
    {
        foreach (Product product in Inventory.Products)
        {
            Console.WriteLine($"ID:{product.ProductId}, Name: {product.ProductName}, Quantity: {product.GetProductQty(Inventory.Products)}, Price: {product.Price}, Cost: {product.ProductCost} Category: {product.ProductCategory}");
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
        decimal price = decimal.Parse(Console.ReadLine());

        Console.Write("Enter Product Cost: ");
        decimal productCost = decimal.Parse(Console.ReadLine());

        Console.Write("Enter Product Category: ");
        string productCategory = Console.ReadLine();

        var newProduct = new Product(productId, productName, price, productCost, productCategory);

        Console.WriteLine("Product Created: ");
        Console.WriteLine(newProduct);
        return newProduct;
    }

    public void ConfirmAdd(Product newProduct)
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