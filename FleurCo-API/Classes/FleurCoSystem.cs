using LibSql;
namespace FleurCo_API.Classes
{
    public class FleurCoSystem
    {
        public Inventory Inventory { get; set; }
        public string ConfirmationMessages { get; set; }
        public SalesForecast SalesForecast { get; set; }
        public Order CurrentOrder { get; set; }
        private LibSqlConnection Connection { get; set; }
        public FleurCoSystem(LibSqlConnection connection)
        {
            Connection = connection;
        }
        public async Task<Rows<InventoryProduct>> DisplayInventory()
        {
            return await Inventory.DisplayInventory(Connection);
        }
        public async Task<Rows<InventoryProduct>> GetItem(string id)
        {
            return await Inventory.GetItem(Connection, id);
        }
        public async Task<Rows<Product>> DisplayProductLine()
        {
            return await Product.DisplayProductLine(Connection);
        }
        public async Task<Rows<Product>> GetProduct(string id)
        {
            return await Product.GetProduct(Connection, id);
        }
        public async Task<Rows<Order>> DisplayOrderList()
        {
            return await Order.DisplayOrderList(Connection);
        }

        public async Task<Rows<Order>> DisplayCustomerOrders()
        {
            return await Order.DisplayCustomerOrders(Connection);
        }
        public async Task<Rows<Order>> DisplayBackOrders()
        {
            return await Order.DisplayBackOrders(Connection);
        }
        public async Task<Rows<Order>> GetOrder(string id)
        {
            return await Order.GetOrder(Connection, id);
        }
        public async Task<Rows<OrderProduct>> GetOrderProducts(string id)
        {
            return await Order.GetOrderProducts(Connection, id);
        }
        public async Task<Product> CreateNewProduct(string productName, double productPrice, double productCost, string productCategory)
        {

            var newProduct = new Product { ProductId = Guid.NewGuid().ToString(), ProductName = productName, ProductPrice = productPrice, ProductCost = productCost, ProductCategory = productCategory };
            await Product.ConfirmAdd(Connection, newProduct);
            await Inventory.AddProduct(Connection, newProduct);
            return newProduct;

        }
        public async Task<Product> ModifyProduct(string productId, string productName, double productPrice, double productCost, string productCategory)
        {

            var modifiedProduct = new Product { ProductId = productId, ProductName = productName, ProductPrice = productPrice, ProductCost = productCost, ProductCategory = productCategory };
            await Product.ConfirmUpdate(Connection, modifiedProduct);
            return modifiedProduct;
        }
        public async Task DeleteProduct(string productId)
        {

            await Product.ConfirmDelete(Connection, productId);

        }

        public async Task UpdateQty(string id, double quantity)
        {
            await Inventory.ConfirmUpdateQty(Connection, id, quantity);
        }
        public async Task<string> CreateBackorder(List<BackOrderPostRequest> newBackOrder)
        {
            var newOrderGuid = await BackOrder.ConfirmBackOrder(Connection, newBackOrder);
            return newOrderGuid;

        }


        // public void SearchProduct()
        // {

        // }

        // public void RequestToGenerateForecast()
        // {

        // }
        // public void SelectForecastCriteria()
        // {

        // }
        // public void SelectProductToScan()
        // {

        // }
        // public void ScanItemCode()
        // {

        // }
        // public void PrintInvoice()
        // {

        // }
        // public void AskConfirmation()
        // {

        // }
        // public void ConfirmPrint()
        // {

        // }
        // public void SelectFleurCoSystem()
        // {

        // }
        // public void SelectFilter()
        // {

        // }
        // public void ProvideItemCode()
        // {

        // }
    }
}
