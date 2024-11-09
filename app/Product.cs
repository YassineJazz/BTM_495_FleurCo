using LibSql;
public class Product
{
    [ColumnName("product_id")] public int ProductId { get; set; }
    [ColumnName("product_code")] public int ProductCode { get; set; }
    [ColumnName("product_name")] public string ProductName { get; set; }
    [ColumnName("product_price")] public double ProductPrice { get; set; }
    [ColumnName("product_cost")] public double ProductCost { get; set; }
    [ColumnName("product_category")] public string ProductCategory { get; set; }
    // public Product(int productId, string productName, double productPrice, double productCost, string productCategory)
    // {
    //     ProductId = productId;
    //     ProductName = productName;
    //     ProductPrice = roductPrice;
    //     ProductCost = productCost;
    //     ProductCategory = productCategory;
    // }
    public void SearchProduct()
    {

    }
    public void SelectProduct()
    {

    }
    public void ConfirmAdd()
    {

    }
    public void ConfirmUpdate()
    {

    }
    public void ConfirmRemoval()
    {

    }
    public int GetProductQty(List<Product> products)
    {
        List<Product> foundProducts = products.FindAll(p => p.ProductCode == ProductCode);
        return foundProducts.Count;
    }
    public void EnterProductQty()
    {

    }
    public void DisplayProductCategory()
    {

    }
}
