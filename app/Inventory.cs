using System.ComponentModel.DataAnnotations;
using LibSql;

public class Inventory
{
    public static async Task<Rows<InventoryProduct>> DisplayInventory(LibSqlConnection connection)
    {
        var inventorySql = "SELECT inventory.inventory_id, products.* FROM inventory INNER JOIN products ON inventory.product_id = products.product_id";
        var inventoryDataRequest = new LibSqlRequest(LibSqlOp.Execute, inventorySql);
        var inventory = await connection.Query<InventoryProduct>([inventoryDataRequest]);
        if (inventory == null)
        {
            throw new InvalidOperationException("No Inventory To Display");
        }
        return inventory;
    }

    public static async Task AddProduct(LibSqlConnection connection, Product newProduct)
    {
        var addSql = @"INSERT INTO Inventory (inventory_id, product_id) VALUES (?,?) ";

        var addArgs = new List<LibSqlArg>
        {
            new LibSqlArg(Guid.NewGuid().ToString()),
            new LibSqlArg(newProduct.ProductId)
        };
        var addDataRequest = new LibSqlRequest(LibSqlOp.Execute, addSql, addArgs);
        await connection.Execute([addDataRequest]);
    }
    public void UpdateProduct()
    {
        //Method unnecessary since, when updating the product, GUI_ID does not change, meaning that the foreign_key in inventory is still the same
    }
    public void DeleteProduct()
    {
        //Method unnecessary since product is automatically removed from inventory when product is removed 
        //(FOREIGN KEY (product_id) REFERENCES Products(product_id) ON DELETE CASCADE)
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
    public string InventoryId { get; set; }
}