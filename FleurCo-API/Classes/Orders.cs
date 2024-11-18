using LibSql;
namespace FleurCo_API.Classes
{
  public class Order
  {

    [ColumnName("order_id")] public string OrderId { get; set; }

    [ColumnName("order_date")] public string OrderDate { get; set; }

    [ColumnName("order_type")] public string OrderType { get; set; }
    [ColumnName("order_status")] public string OrderStatus { get; set; }
    [ColumnName("order_total")] public double OrderTotal { get; set; }


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
    public static async Task<Rows<Order>> DisplayCustomerOrders(LibSqlConnection connection)
    {
      var orderSql = "SELECT * FROM Orders WHERE order_type = 'customer'";
      var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql);
      var orders = await connection.Query<Order>([orderDataRequest]);
      if (orders == null)
      {
        throw new InvalidOperationException("No Customer Orders Found");
      }
      return orders;
    }
    public static async Task<Rows<Order>> DisplayBackOrders(LibSqlConnection connection)
    {
      var orderSql = "SELECT * FROM Orders WHERE order_type = 'backorder'";
      var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql);
      var orders = await connection.Query<Order>([orderDataRequest]);
      if (orders == null)
      {
        throw new InvalidOperationException("No Orders Backorders Found");
      }
      return orders;
    }
    public static async Task<Rows<Order>> GetOrder(LibSqlConnection connection, string id)
    {
      var orderSql = "SELECT * FROM Orders WHERE order_id = ?";

      var orderArgs = new List<LibSqlArg>
        {
            new LibSqlArg(id)
        };
      var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql, orderArgs);
      var order = await connection.Query<Order>([orderDataRequest]);
      if (order == null)
      {
        throw new InvalidOperationException("No Orders To Display");
      }
      return order;
    }
    public static async Task<Rows<OrderProduct>> GetOrderProducts(LibSqlConnection connection, string id)
    {
      var selectedOrderSql = "SELECT Orders.*, OrderProducts.product_qty, Products.* FROM Orders JOIN OrderProducts ON Orders.order_id = OrderProducts.order_id JOIN Products on OrderProducts.product_id = Products.product_id WHERE Orders.order_id = ?";
      var selectedOrderArgs = new List<LibSqlArg>
        {
            new LibSqlArg(id)
        };
      var selectedOrderDataRequest = new LibSqlRequest(LibSqlOp.Execute, selectedOrderSql, selectedOrderArgs);
      var selectedOrder = await connection.Query<OrderProduct>([selectedOrderDataRequest]);
      if (selectedOrder == null)
      {
        throw new InvalidOperationException("No Order Products to Display");
      }
      return selectedOrder;
    }
    // public virtual void SelectProduct()
    // {

    // }
    // public void SelectProductToScan()
    // {

    // }
    // public void ItemScannedInformation()
    // {

    // }
    // public void GetPastOrders()
    // {

    // }
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

    // public void RemoveProduct()
    // {

    // }
    // public void AddtoCart()
    // {

    // }
    // public void CancelOrder()
    // {

    // }
    public static async Task<string> ConfirmBackOrder(LibSqlConnection connection, List<BackOrderPostRequest> newBackOrder)
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

      var addBackOrderSql = @"INSERT INTO BackOrders (backorder_id, whm_id, backorder_cost) VALUES (?,'1',?) ";

      var addBackOrderArgs = new List<LibSqlArg>
        {
            new LibSqlArg(newOrderGuid),
            new LibSqlArg(backOrderTotal)

        };
      var addBackOrderDataRequest = new LibSqlRequest(LibSqlOp.Execute, addBackOrderSql, addBackOrderArgs);
      await connection.Execute([addBackOrderDataRequest]);

      return newOrderGuid;
    }
    // public override void SelectProduct()
    // {

    // }
  }
}