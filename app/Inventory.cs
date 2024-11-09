using LibSql;

public class Inventory
{

    public static async Task<Rows<InventoryProduct>> DisplayInventory(LibSqlConnection connection)
    {
        var inventorySql = "SELECT inventory.inventory_id, products.* FROM inventory INNER JOIN products ON inventory.product_id = products.product_id";
        var inventoryDataRequest = new LibSqlRequest(LibSqlOp.Execute, inventorySql);
        var inventoryCloseRequest = new LibSqlRequest(LibSqlOp.Close);
        var inventory = await connection.Query<InventoryProduct>([inventoryDataRequest, inventoryCloseRequest]);
        if (inventory == null)
        {
            throw new InvalidOperationException("No Inventory To Display");
        }
        return inventory;
    }
    public void AddProduct()
    {

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
public class InventoryProduct : Product
{
    [ColumnName("inventory_id")]
    public int InventoryId { get; set; }
}