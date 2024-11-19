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
      var orders = await connection.Query<Order>([orderDataRequest, new(LibSqlOp.Close)]);
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
      var orders = await connection.Query<Order>([orderDataRequest, new(LibSqlOp.Close)]);
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
      var orders = await connection.Query<Order>([orderDataRequest, new(LibSqlOp.Close)]);
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
            new(id)
        };
      var orderDataRequest = new LibSqlRequest(LibSqlOp.Execute, orderSql, orderArgs);
      var order = await connection.Query<Order>([orderDataRequest, new(LibSqlOp.Close)]);
      if (order == null)
      {
        throw new InvalidOperationException("No Orders To Display");
      }
      return order;
    }
    public static async Task<Rows<OrderProduct>> GetOrderProducts(LibSqlConnection connection, string id)
    {
      var selectedOrderSql = "SELECT OrderProducts.product_qty, OrderProducts.inventory_id, Products.*, Inventory.* FROM OrderProducts JOIN Products ON Inventory.product_id = Products.product_id JOIN Inventory ON OrderProducts.inventory_id = Inventory.inventory_id WHERE OrderProducts.order_id = ?";
      var selectedOrderArgs = new List<LibSqlArg>
        {
            new(id)
        };
      var selectedOrderDataRequest = new LibSqlRequest(LibSqlOp.Execute, selectedOrderSql, selectedOrderArgs);
      var selectedOrder = await connection.Query<OrderProduct>([selectedOrderDataRequest, new(LibSqlOp.Close)]);
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
  public class OrderProduct : Product
  {
    [ColumnName("inventory_id")] public string inventoryId { get; set; }
    [ColumnName("product_qty")] public double ProductQty { get; set; }
  }


  public class CustomerOrder : Order
  {
    public string CustomerID { get; set; }
    public string WorkerID { get; set; }
  }


  class Association
  {
    public double Quantity { get; set; }
    public double Cost { get; set; }

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
    public static async Task<string> ConfirmBackOrder(LibSqlConnection connection, Rows<InventoryProduct> newBackOrder, List<BackOrderPostRequest> request)
    {
      var newOrderGuid = Guid.NewGuid().ToString();

      double backOrderTotal = 0;

      var itemMap = new Dictionary<string, Association>();
      foreach (var item in newBackOrder)
      {
        itemMap[item.InventoryId] = new Association { Cost = item.ProductCost, Quantity = 0 };
      }

      foreach (var item in request)
      {
        itemMap[item.InventoryId].Quantity = item.Quantity;
      }

      foreach (var (key, value) in itemMap)
      {
        backOrderTotal += value.Cost * value.Quantity;
      }

      var addOrderSql = @"INSERT INTO Orders (order_id, order_type, order_status, order_total) VALUES (?,'backorder','in progress',?) ";

      var addOrderArgs = new List<LibSqlArg>
        {
            new(newOrderGuid),
            new(backOrderTotal)

        };
      var addOrderDataRequest = new LibSqlRequest(LibSqlOp.Execute, addOrderSql, addOrderArgs);
      await connection.Execute([addOrderDataRequest]);

      foreach (var item in request)
      {
        var addOrderProductSql = @"INSERT INTO OrderProducts (order_id, inventory_id, product_qty) VALUES (?,?,?) ";
        var addOrderProductArgs = new List<LibSqlArg>
        {
            new(newOrderGuid),
            new(item.InventoryId),
            new(item.Quantity)

        };
        var addOrderProductDataRequest = new LibSqlRequest(LibSqlOp.Execute, addOrderProductSql, addOrderProductArgs);
        await connection.Execute([addOrderProductDataRequest]);
      }

      var addBackOrderSql = @"INSERT INTO BackOrders (backorder_id, whm_id, backorder_cost) VALUES (?,'1',?) ";

      var addBackOrderArgs = new List<LibSqlArg>
        {
            new(newOrderGuid),
            new(backOrderTotal)

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