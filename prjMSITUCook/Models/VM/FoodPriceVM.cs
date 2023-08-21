namespace prjMSITUCook.Models.VM
{
    public class FoodPriceVM
    {
        public FoodPriceVM() {
            PriceDetail = new List<FoodPriceEveryMarketVM>();
        }
        public string CropName { get; set; }

        public List<FoodPriceEveryMarketVM> PriceDetail { get; set; }
    }
}
