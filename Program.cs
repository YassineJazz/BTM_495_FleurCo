public class FleurCoSystem
{
    public List<Order> Orders {get;set;}
    public List<Invoice> Invoices {get;set;}
    public Inventory Inventory {get;set;}
    public string ConfirmationMessages {get;set;}
    public SalesForecast SalesForecast {get; set;}
    public Order CurrentOrder {get;set;}

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
            Console.WriteLine($"ID:{product.ProductId}, Name: {product.ProductName}, Quantity: {product.Quantity}, Price: {product.Price}, Cost: {product.ProductCost} Category: {product.ProductCategory}");
        }
    }
    public void SearchProduct()
    {
        
    }
    public void SelectProduct()
    {
        
    }
  
    public void CreateNewProduct()
    {
        
    }
    public void ModifyProduct()
    {
        
    }
    public void RemoveProduct()
    {
        
    }
    public void RequestForecast()
    {
        
    }
    public void SelectForecastCriteria()
    {
        
    }
    public void GetSalesForecast()
    {
        
    }
    public void ReadItemCode()
    {
        
    }
      public void DisplayOrderList()
    {
        
    }
    public void SelectOrder()
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
        
    

    
}
public class SalesForecast 
{
    public DateTime TimeFrame {get; set;}
    public int ForecastStockLevel {get; set;}
    public string ProductCategory {get; set;}
    public List<SalesForecast> ForecastHistory {get; set;}

    public SalesForecast (DateTime timeframe, int forecaststocklevel, string productcategory, List<SalesForecast> forecasthistory)
    {
        TimeFrame = timeframe;
        ForecastStockLevel = forecaststocklevel;
        ProductCategory = productcategory;
        ForecastHistory = forecasthistory;
    }

    public void DisplayForecastedLevel()
    {

    }

    public void SaveForecast()
    {

    }
}


public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal ProductCost {get;set;}
    public string ProductCategory{get;set;}

    public Product(int productId, string productName, int quantity, decimal price, decimal productCost, string productCategory)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        ProductCost = productCost;
        ProductCategory = productCategory;
    }
    public void ConfirmAdd()
    {

    }
    public void ConfirmModify()
    {

    }
    public void ConfirmRemove()
    {

    }
    
}

public class Inventory 
{

    public List<Product> Products {get;set;}

    public int ProductQty {get;set;}
    {
        Products = new List<Product>();
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

public class Order
{

  public int OrderID{get;set;}
  public List<Product> Products {get;set;}
  public string OrderType{get;set;}
  public string OrderStatus{get;set;}
  public decimal OrderTotal{get;set;}
  public List<Orders> PastOrders {get;set;}  

  public Order(int orderid, List<Product> products, string ordertype, string orderstatus, List<Orders> pastorders){
    OrderID = orderid;
    Products = products;
    OrderType = ordertype;
    OrderStatus = orderstatus;
    PastOrders = pastorders;  
    CalculateOrderTotal();

void CalculateOrderTotal()
      {
          decimal total = 0;

          foreach (var product in Products)
          {
              total += product.Price * product.Quantity;
          }

          OrderTotal = total;  
      }
    }
  public void DisplayCustomerOrder()
    {
        
    Console.WriteLine($"Order number : {OrderID} of type {OrderType}. Consists of :");
      foreach (var product in Products)
      {
          Console.WriteLine($"- Product: {product.ProductName}, Quantity: {product.Quantity}, Price: {product.Price:C}");
      }
        Console.WriteLine($"This order is currently {OrderStatus}");
        foreach(var product in Products){
           
        
            }
        Console.WriteLine($"The order total is : {OrderTotal:C}");
  }
    public void DisplayOrderList()
    {
        
    }
    public void DisplayOrderOverview()
    {
        
    }
    public void DisplayBackOrder()
    {

    }
    
    public void SelectProduct()
    {
        
    }
    public void SelectProductToScan()
    {
        
    }
    public void GetPastOrders()
    {

    }
}
public class OrderProduct
{
  public int OrderID {get;set;}
  public int ProductID {get;set;}
  public int ProductQuantity {get;set;}

  public OrderProduct(int orderid, int productid, int productquantity)
  {
    OrderID = orderid;
    ProductID = productid;
    ProductQuantity = productquantity;
  }
}
public class Invoice
{
    public int InvoiceID {get;set;}
    public decimal TotalPrice {get;set;}
    public DateTime Date {get;set;}
    public int OrderID {get;set;}

    public Invoice(int invoiceid, decimal totalprice, DateTime date, int orderid)
    {
        InvoiceID = invoiceid;
        TotalPrice = totalprice;
        Date = date;
        OrderID = orderid;
    }
    public void PrintInvoice()
    {
        
    }
    
}

public class Address
{
    public string Street {get;set;}
    public string Municipality {get;set;}
    public string Province {get;set;}
    public string Country {get;set;}
    public string PostalCode {get;set;}
    public string Type {get;set;}

    public Address (string street, string municipality, string province, string country, string postalCode, string type)
    {
        Street = street;
        Municipality = municipality;
        Province = province;
        Country = country;
        PostalCode = postalCode;
        Type = type;
    }

}
public class User
{
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Email {get;set;}
    public string MainPhoneNumber {get;set;}

    public User(string firstname, string lastname, string email, string mainphonenumber)
    {
    FirstName = firstname;
    LastName = lastname;
    Email = email;
    MainPhoneNumber = mainphonenumber;
    }
}
public class WHManager : User
{
    public int WHManagerID {get;set;}

    public WHManager(int whmanagerid, string firstname, string lastname, string email, string mainphonenumber)
    :base ( firstname,  lastname,  email, mainphonenumber){
        WHManagerID = whmanagerid;
    }
    
}
public class WHWorker : User
{
    public int WHWorkerID {get;set;}

    public WHWorker(int whworkerid, string firstname, string lastname, string email, string mainphonenumber)
    :base ( firstname,  lastname,  email, mainphonenumber){
        WHWorkerID = whworkerid;
        }
   
    
}
public class Customer : User
{
    public int CustomerID {get;set;}
    public string FaxNumber{get;set;}

    public Customer(int customerid, string firstname, string lastname, string email, string mainphonenumber, string faxnumber)
    :base ( firstname,  lastname,  email, mainphonenumber){
        CustomerID = customerid;
        FaxNumber = faxnumber;
        }
   
}
public class NewOrder : Order 
{
    public int CustomerID {get; set;}
    public int WorkerID {get; set;}

    public NewOrder(int customerid, int workerid): 
    base (orderid, products, ordertype, orderstatus, pastorders)
    {
        CustomerID = customerid;
        WorkerID = workerid;
    }
}

public class BackOrder : Order
{
    public int WHManagerID {get; set;}

    public BackOrder (int whmanagerid) : 
    base(orderid, products, ordertype, orderstatus, pastorders)
    {
        WHManagerID = whmanagerid;
    }
    public void RemoveProduct()
    {

    }
    public void AddtoCart()
    {

    }
    public void CancelOrder()
    {

    }
    public void ConfirmOrder()
    {

    }
    public void ConfirmOrder()
    {

    }
    public void SelectProduct()
    {
    
    }
}
