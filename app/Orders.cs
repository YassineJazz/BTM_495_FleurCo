using LibSql;

public class Order
{

  [ColumnName("order_id")] public string OrderID { get; set; }
  public List<Product> Products { get; set; }
  [ColumnName("order_type")] public string OrderType { get; set; }
  [ColumnName("order_status")] public string OrderStatus { get; set; }
  [ColumnName("order_total")] public double OrderTotal { get; set; }
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
  public static async Task<Rows<Order>> DisplayOrderList(LibSqlConnection connection)
  {
    var orderSql = "SELECT * FROM Orders";
    var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql);
    var orders = await connection.Query<Order>([orderDataRequest]);
    if (orders == null)
    {
      throw new InvalidOperationException("No Orders Found");
    }
    return orders;

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
  public string OrderID { get; set; }
  public string ProductID { get; set; }
  public int ProductQuantity { get; set; }

  public OrderProduct(string orderid, string productid, int productquantity)
  {
    OrderID = orderid;
    ProductID = productid;
    ProductQuantity = productquantity;
  }
}

public class NewOrder : Order
{
  public string CustomerID { get; set; }
  public string WorkerID { get; set; }
}

public class BackOrder : Order
{
  public string WHManagerID { get; set; }

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