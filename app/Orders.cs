public class Order
{

  public int OrderID { get; set; }
  public List<Product> Products { get; set; }
  public string OrderType { get; set; }
  public string OrderStatus { get; set; }
  public decimal OrderTotal { get; set; }
  public List<Order> PastOrders { get; set; }

  public Order(int orderid, List<Product> products, string ordertype, string orderstatus, List<Order> pastorders)
  {
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
        total += product.Price * product.GetProductQty(Products);
      }

      OrderTotal = total;
    }
  }
  public void DisplayCustomerOrder()
  {

    Console.WriteLine($"Order number : {OrderID} of type {OrderType}. Consists of :");
    foreach (var product in Products)
    {
      Console.WriteLine($"- Product: {product.ProductName}, Quantity: {product.GetProductQty(Products)}, Price: {product.Price:C}");
    }
    Console.WriteLine($"This order is currently {OrderStatus}");


    Console.WriteLine($"The order total is : {OrderTotal:C}");
  }
  public void DisplayBackOrder()
  {

  }
  public void DisplayOrderList()
  {

  }
  public void DisplayOrderOverview()
  {

  }
  public virtual void SelectProduct()
  {

  }
  public void SelectProductToScan()
  {

  }
  public void ItemScannedInformation()
  {

  }
  public void GetPastOrders()
  {

  }
}
public class OrderProduct
{
  public int OrderID { get; set; }
  public int ProductID { get; set; }
  public int ProductQuantity { get; set; }

  public OrderProduct(int orderid, int productid, int productquantity)
  {
    OrderID = orderid;
    ProductID = productid;
    ProductQuantity = productquantity;
  }
}

public class NewOrder : Order
{
  public int CustomerID { get; set; }
  public int WorkerID { get; set; }

  public NewOrder(int customerid, int workerid, int orderid, List<Product> products, string ordertype, string orderstatus, List<Order> pastorders) :
  base(orderid, products, ordertype, orderstatus, pastorders)
  {
    CustomerID = customerid;
    WorkerID = workerid;
  }
}

public class BackOrder : Order
{
  public int WHManagerID { get; set; }

  public BackOrder(int whmanagerid, int orderid, List<Product> products, string ordertype, string orderstatus, List<Order> pastorders) :
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
  public void ConfirmBackOrder()
  {

  }
  public override void SelectProduct()
  {

  }
}