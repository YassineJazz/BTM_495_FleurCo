using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
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
    public void DisplaySalesForecast()
    {
        
    }
  
    public void DisplayCriteria()
    {
        
    }
    public void DisplayCustomerOrder()
    {
        
    }
    public void DisplayOrderList()
    {
        
    }
    public void DisplayOrderOverview()
    {
        
    }
    public void DisplayConfirmationMessage()
    {
        
    }
    public void PrintInvoice()
    {
        
    }
    public void AskConfirmation()
    {
        
    }
      public void SelectOrder()
    {
        
    }
    public void ConfirmPrint()
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
    public void SearchProduct()
    {
        
    }
    public void SelectProduct()
    {
        
    }
  
        public void DisplayInventory()
    {
        foreach (Product product in Inventory.Products)
        {
            Console.WriteLine($"ID:{product.ProductId}, Name: {product.ProductName}, Quantity: {product.Quantity}, Price: {product.Price}, Cost: {product.ProductCost} Category: {product.ProductCategory}");
        }
    }
    

    
}
public class SalesForecast 
{
    public DateTime TimeFrame {get; set;}
    public int ForecastStockLevel {get; set;}
    public string ProductCategory {get; set;}

    public SalesForecast (DateTime timeframe, int forecaststocklevel, string productcategory)
    {
        TimeFrame = timeframe;
        ForecastStockLevel = forecaststocklevel;
        ProductCategory = productcategory;
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
    public void AddOrder()
    {
        
    }
    public void SaveChanges()
    {

    }

    
}

public class Inventory 
{

    public List<Product> Products {get;set;}

    public Inventory()
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

}

public class Order
{

  public int OrderID{get;set;}
  public List<Product> Products {get;set;}
  public string OrderType{get;set;}
  public string OrderStatus{get;set;}
  public decimal OrderTotal{get;set;}  

  public Order(int orderid, List<Product> products, string ordertype, string orderstatus){
    OrderID = orderid;
    Products = products;
    OrderType = ordertype;
    OrderStatus = orderstatus;
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
    public void RemoveProduct()
    {
        
    }
    public void AddtoCart()
    {
        
    }
    public void ConfirmOrder()
    {
        
    }
    public void CancelOrder()
    {
        
    }
}
public class OrderProduct
{
  public int OrderID {get;set;}
  public int ProductID {get;set;}
  public string ProductName {get;set;}
  public int ProductQuantity {get;set;}
  public decimal ProductPrice {get;set;}

  public OrderProduct(int orderid, int productid, string productname, int productquantity, decimal productprice)
  {
    OrderID = orderid;
    ProductID = productid;
    ProductName = productname;
    ProductQuantity = productquantity;
    ProductPrice = productprice;
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
    public void PlaceBackOrder()
    {
        
    }
   
    public void ConfirmOrderInvoice()
    {
        
    }
    public void ModifyOrderInvoice()
    {
        
    }
    public void CancelOrderInvoiceSelection()
    {
        
    }
    public void ScanItemCode()
    {
        
    }
    public void InputItemCode()
    {
        
    }
    public void GenerateSalesForecast()
    {
        
    }
}
public class WHWorker : User
{
    public int WHWorkerID {get;set;}

    public WHWorker(int whworkerid, string firstname, string lastname, string email, string mainphonenumber)
    :base ( firstname,  lastname,  email, mainphonenumber){
        WHWorkerID = whworkerid;
        }
    public void ConfirmOrderInvoice()
    {
        
    }
    public void ModifyOrderInvoice()
    {
        
    }
    public void CancelOrderInvoiceSelection()
    {
        
    }
    public void ScanItemCode()
    {
        
    }
    public void InputItemCode()
    {
        
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
    public void PlaceNewOrder()
    {
        
    }
}
public class NewOrder : Order 
{
    public int CustomerID {get; set;}
    public int WorkerID {get; set;}

    public NewOrder(int customerid, int workerid, int orderid, List<Product> products, string ordertype, string orderstatus): 
    base (orderid, products, ordertype, orderstatus)
    {
        CustomerID = customerid;
        WorkerID = workerid;
    }
}

public class BackOrder : Order
{
    public int WHManagerID {get; set;}
    public decimal TotalCost{get; set;}

    public BackOrder (int whmanagerid, int orderid, List<Product> products, string ordertype, string orderstatus) : 
    base(orderid, products, ordertype, orderstatus)
    {
        WHManagerID = whmanagerid;
        CalculateTotalCost();
void CalculateTotalCost()
        {
              decimal totalcost = 0;

              foreach (var product in Products)
              {
                  totalcost += product.ProductCost * product.Quantity;
              }

              TotalCost = totalcost;  
        }
    }

}
