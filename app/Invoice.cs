using LibSql;

public class Invoice
{
    [ColumnName("invoice_id")] public string InvoiceID { get; set; }
    [ColumnName("total_price")] public double TotalPrice { get; set; }
    [ColumnName("invoice_date")] public DateTime Date { get; set; }
    public int OrderID { get; set; }
    public void PrintInvoice()
    {

    }
}