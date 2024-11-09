public class Order
{

  public int OrderID { get; set; }
  public List<Product> Products { get; set; }
  public string OrderType { get; set; }
  public string OrderStatus { get; set; }
  public double OrderTotal { get; set; }
  public List<Order> PastOrders { get; set; }

  public void CalculateOrderTotal()
  {
    double total = 0;

    foreach (var product in Products)
    {
      total += product.ProductPrice * product.GetProductQty(Products);
    }

    OrderTotal = total;
  }
  public void DisplayCustomerOrder()
  {

    Console.WriteLine($"Order number : {OrderID} of type {OrderType}. Consists of :");
    foreach (var product in Products)
    {
      Console.WriteLine($"- Product: {product.ProductName}, Quantity: {product.GetProductQty(Products)}, Price: {product.ProductPrice:C}");
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
}

public class BackOrder : Order
{
  public int WHManagerID { get; set; }

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