using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Dto.ResultModel
{
    public class FoodPriceDetailResultModel
    {
        public string MarketName { get; set; }
        public float UpperPrice { get; set; }
        public float LowerPrice { get; set; }
        public string Unit { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
