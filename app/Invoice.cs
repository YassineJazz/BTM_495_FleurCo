public class Invoice
{
    public int InvoiceID { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; }
    public int OrderID { get; set; }

    public Invoice(int invoiceid, decimal totalprice, DateTime date, int orderid)
    {
        InvoiceID = invoiceid;
        TotalPrice = totalprice;
        Date = date;
        OrderID = orderid;
    }
    public void PrintInvoice()
    {

    }

}