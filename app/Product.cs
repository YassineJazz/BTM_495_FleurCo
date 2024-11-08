public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal ProductCost {get;set;}
    public string ProductCategory{get;set;}
    public Product(int productId, string productName, decimal price, decimal productCost, string productCategory)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
        ProductCost = productCost;
        ProductCategory = productCategory;
    }
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
    public int GetProductQty(List<Product> products){
        List<Product> foundProducts = products.FindAll(p => p.ProductId == ProductId);
        return foundProducts.Count;
    }
    public void EnterProductQty()
    {
        
    }
    public void DisplayProductCategory()
    {

    }
}
