using LibSql;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace FleurCo_API.Classes
{

    public class Inventory
    {
        public static async Task<Rows<InventoryProduct>> DisplayInventory(LibSqlConnection connection)
        {
            var inventorySql = "SELECT inventory.inventory_id, products.*, inventory.quantity FROM inventory INNER JOIN products ON inventory.product_id = products.product_id";
            var inventoryDataRequest = new LibSqlRequest(LibSqlOp.Execute, inventorySql);
            var inventory = await connection.Query<InventoryProduct>([inventoryDataRequest]);
            if (inventory == null)
            {
                throw new InvalidOperationException("No Inventory To Display");
            }
            return inventory;
        }
        public static async Task<Rows<InventoryProduct>> GetItem(LibSqlConnection connection, string id)
        {
            var itemSql = "SELECT inventory.inventory_id, products.*, inventory.quantity FROM inventory INNER JOIN products ON inventory.product_id = products.product_id WHERE inventory.inventory_id = ?";

            var itemArgs = new List<LibSqlArg>
        {
            new LibSqlArg(id)
        };
            var itemDataRequest = new LibSqlRequest(LibSqlOp.Execute, itemSql, itemArgs);
            var item = await connection.Query<InventoryProduct>([itemDataRequest]);
            if (item == null)
            {
                throw new InvalidOperationException("No Items To Display");
            }
            return item;
        }

        public static async Task AddProduct(LibSqlConnection connection, Product newProduct)
        {
            var addSql = @"INSERT INTO Inventory (inventory_id, product_id, quantity) VALUES (?,?,1) ";

            var addArgs = new List<LibSqlArg>
        {
            new LibSqlArg(Guid.NewGuid().ToString()),
            new LibSqlArg(newProduct.ProductId)
        };
            var addDataRequest = new LibSqlRequest(LibSqlOp.Execute, addSql, addArgs);
            await connection.Execute([addDataRequest]);
        }
        public static async Task ConfirmUpdateQty(LibSqlConnection connection, string id, double quantity)
        {
            var updateQtySql = @"UPDATE Inventory SET quantity = ? WHERE inventory_id = ?";
            var updateQtyArgs = new List<LibSqlArg>
        {
            new LibSqlArg(quantity),
            new LibSqlArg(id)
        };
            var updateQtyRequest = new LibSqlRequest(LibSqlOp.Execute, updateQtySql, updateQtyArgs);
            await connection.Execute([updateQtyRequest]);


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

        // public void IncreaseProductQty()
        // {

        // }
        // public void DecreaseProdQty()
        // {

        // }
        // public void RetrieveItemData()
        // {

        // }
    }
    public class InventoryProduct : Product
    {
        [ColumnName("inventory_id")]
        [JsonPropertyName("inventoryId")]
        public string InventoryId { get; set; }
        [ColumnName("quantity")]
        [JsonPropertyName("quantity")]
        public double Quantity { get; set; }
    }
}