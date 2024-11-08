public class SalesForecast 
{
    public DateTime TimeFrame {get; set;}
    public int ForecastStockLevel {get; set;}
    public string ProductCategory {get; set;}
    public List<SalesForecast> ForecastHistory {get; set;}

    public SalesForecast (DateTime timeframe, int forecaststocklevel, string productcategory, List<SalesForecast> forecasthistory)
    {
        TimeFrame = timeframe;
        ForecastStockLevel = forecaststocklevel;
        ProductCategory = productcategory;
        ForecastHistory = forecasthistory;
    }
    public void GetSalesForecast()
    {
        
    }
    public void DisplayForecastedLevel()
    {

    }

    public void SaveForecast()
    {

    }
}