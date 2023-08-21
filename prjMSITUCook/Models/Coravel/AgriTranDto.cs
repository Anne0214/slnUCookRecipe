namespace prjMSITUCook.Models.Coravel
{
    public class AgriTranDto
    {
        public string CropCode { get; set; }
        public string CropName { get; set; }
        public string MarketName { get; set; }
        public string TransDate { get; set; }
        public string MarketCode { get; set; }
        public float Upper_Price { get; set; }
        public float Middle_Price { get; set; }
        public float Lower_Price { get; set; }
        public float Avg_Price { get; set; }
        public float Trans_Quantity { get; set; }
    }
}
