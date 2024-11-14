using LibSql;

public class Order
{

  [ColumnName("order_id")] public string OrderId { get; set; }
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

  public static async Task<Rows<OrderProduct>> DisplayOrderList(LibSqlConnection connection)
  {
    var orderSql = "SELECT * FROM Orders";
    var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql);
    var orders = await connection.Query<OrderProduct>([orderDataRequest]);
    if (orders == null)
    {
      throw new InvalidOperationException("No Orders Found");
    }
    return orders;
  }
  public static async Task<Rows<OrderProduct>> DisplayCustomerOrders(LibSqlConnection connection)
  {
    var orderSql = "SELECT * FROM Orders WHERE order_type = 'customer'";
    var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql);
    var orders = await connection.Query<OrderProduct>([orderDataRequest]);
    if (orders == null)
    {
      throw new InvalidOperationException("No Customer Orders Found");
    }
    return orders;
  }
  public static async Task<Rows<OrderProduct>> DisplayBackOrders(LibSqlConnection connection)
  {
    var orderSql = "SELECT * FROM Orders WHERE order_type = 'backorder'";
    var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql);
    var orders = await connection.Query<OrderProduct>([orderDataRequest]);
    if (orders == null)
    {
      throw new InvalidOperationException("No Orders Backorders Found");
    }
    return orders;
  }
  public static async Task<Rows<OrderProduct>> DisplaySelectedOrder(LibSqlConnection connection, OrderProduct orderToSelect)
  {
    var selectedOrderSql = "SELECT Orders.*, OrderProducts.product_qty, Products.* FROM Orders JOIN OrderProducts ON Orders.order_id = OrderProducts.order_id JOIN Products on OrderProducts.product_id = Products.product_id WHERE Orders.order_id = ?";
    var selectedOrderArgs = new List<LibSqlArg>
        {
            new LibSqlArg(orderToSelect.OrderId)
        };
    var selectedOrderDataRequest = new LibSqlRequest(LibSqlOp.Execute, selectedOrderSql, selectedOrderArgs);
    var selectedOrder = await connection.Query<OrderProduct>([selectedOrderDataRequest]);
    if (selectedOrder == null)
    {
      throw new InvalidOperationException("No Selected Order To Display");
    }
    return selectedOrder;
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
public class OrderProduct : Order
{
  [ColumnName("product_id")]
  public string ProductId { get; set; }
  [ColumnName("product_name")]
  public string ProductName { get; set; }
  [ColumnName("product_price")]
  public double ProductPrice { get; set; }
  [ColumnName("product_cost")]
  public double ProductCost { get; set; }
  [ColumnName("product_category")]
  public string ProductCategory { get; set; }
  [ColumnName("product_qty")]
  public double ProductQuantity { get; set; }
}


public class CustomerOrder : Order
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
  public static async Task ConfirmBackOrder(LibSqlConnection connection, List<InventoryProduct> newBackOrder)
  {
    var newOrderGuid = Guid.NewGuid().ToString();

    double backOrderTotal = 0;
    foreach (var item in newBackOrder)
    {
      backOrderTotal += item.ProductCost * item.Quantity;
    }
    var addOrderSql = @"INSERT INTO Orders (order_id, order_type, order_status, order_total) VALUES (?,'backorder','in progress',?) ";

    var addOrderArgs = new List<LibSqlArg>
        {
            new LibSqlArg(newOrderGuid),
            new LibSqlArg(backOrderTotal)

        };
    var addOrderDataRequest = new LibSqlRequest(LibSqlOp.Execute, addOrderSql, addOrderArgs);
    await connection.Execute([addOrderDataRequest]);

    foreach (var item in newBackOrder)
    {
      var addOrderProductSql = @"INSERT INTO OrderProducts (order_id, product_id, product_qty) VALUES (?,?,?) ";
      var addOrderProductArgs = new List<LibSqlArg>
        {
            new LibSqlArg(newOrderGuid),
            new LibSqlArg(item.ProductId),
            new LibSqlArg(item.Quantity)

        };
      var addOrderProductDataRequest = new LibSqlRequest(LibSqlOp.Execute, addOrderProductSql, addOrderProductArgs);
      await connection.Execute([addOrderProductDataRequest]);
    }

    var addBackOrderSql = @"INSERT INTO BackOrders (backorder_id, whm_id, user_id, backorder_cost) VALUES (?,'1','1',?) ";

    var addBackOrderArgs = new List<LibSqlArg>
        {
            new LibSqlArg(newOrderGuid),
            new LibSqlArg(backOrderTotal)

        };
    var addBackOrderDataRequest = new LibSqlRequest(LibSqlOp.Execute, addBackOrderSql, addBackOrderArgs);
    await connection.Execute([addBackOrderDataRequest]);
  }
  public override void SelectProduct()
  {

  }
}