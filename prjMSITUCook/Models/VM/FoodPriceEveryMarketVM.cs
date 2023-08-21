namespace prjMSITUCook.Models.VM
{
    public class FoodPriceEveryMarketVM
    {
        public string MarketName { get; set; }
        public float UpperPrice { get; set; }
        public float LowerPrice { get; set; }
        public string Unit { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
