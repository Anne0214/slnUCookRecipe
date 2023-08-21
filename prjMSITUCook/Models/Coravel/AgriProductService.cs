using Microsoft.Extensions.Caching.Distributed;
using prjMSITUCook.Models.VM;
using prjMSITUCookServices.Dto.ResultModel;
using System.Net.Http.Headers;
using System.Text;

namespace prjMSITUCook.Models.Coravel
{
    public class AgriProductService
    {
        private readonly IDistributedCache _cache;


        public AgriProductService(IDistributedCache cache) {
            _cache = cache;
        }
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        public async Task<List<AgriTranDto>> GetAgriProductTransData() {
            string uri = "https://data.moa.gov.tw/api/v1/AgriProductsTransType/?Start_time=112.08.11&End_time=112.08.11";
            //單個打api
            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Task<AgriProductsTransTypeResponseDto> tranList = httpClient.GetFromJsonAsync<AgriProductsTransTypeResponseDto>(uri);

            var result = await tranList;
            if (result.Data == null || result.Data.Count() <= 0)
            {
                return null;
            }
            return result.Data;
        }

        /// <summary>
        /// 存入快取
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ImportToCache() {
            try { 
                //拿到資料
                var task = GetAgriProductTransData();
                var data = await task;
                var group = data.GroupBy(x=>x.CropName);
                StringBuilder sb = new StringBuilder();
                foreach (var i in group) {
                    //處理string
                    foreach (var s in i) {
                        string temp = BuildRedisValueString(s);
                        temp = temp.Remove(temp.Length-1,1); //刪除最後一個逗號
                        sb.Append(i.Key);
                        sb.Append(",");
                        sb.Append(temp);
                        sb.Append(";"); //小黃瓜,市場,最低價,最高價,...;小黃瓜,市場，最低價,最高價...
                    }
                    sb.Remove(sb.Length-1,1); //刪除最後一個分號
                    sb.Append('%');// 小黃瓜,市場...;小黃瓜,市場...;
                }

                await _cache.SetStringAsync("AgriProductTrans", sb.ToString(), new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddDays(7)
                }) ;
                return true;
            }
            catch {
                return false;
            }
        }
        /// <summary>
        /// 把AgriTranDto弄成一個可以拆解的string(市場,最低價,最高價,單位,更新時間/用分號分隔)，存入快取
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string BuildRedisValueString(AgriTranDto item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.MarketName);
            sb.Append(",");

            sb.Append(item.Lower_Price.ToString());
            sb.Append(",");

            sb.Append(item.Upper_Price.ToString());
            sb.Append(",");

            sb.Append("元/公斤");
            sb.Append(",");

            sb.Append(DateTime.Now.ToString());
            sb.Append(",");

            return sb.ToString();
        }
        public List<FoodPriceVM> DecomposeRadiusValuseForAgriProductTrans(string value) {
            var result = new List<FoodPriceVM>();
            var cropNames = value.Split("%").ToList(); //分出不同食材
            cropNames.RemoveAll(x => string.IsNullOrEmpty(x));
            foreach (var crop in cropNames) {
                var markets = crop.Split(";").ToList(); //分出不同市場的紀錄
                markets.RemoveAll(x => string.IsNullOrEmpty(x));
                FoodPriceVM vm = new FoodPriceVM();
                foreach (var market in markets) {
                    FoodPriceEveryMarketVM item = new FoodPriceEveryMarketVM();
                    var data = market.Split(",").ToList();
                    data.RemoveAll(x => string.IsNullOrEmpty(x));
                    vm.CropName = data[0];
                    item.MarketName = data[1];
                    item.LowerPrice = float.Parse(data[2]);
                    item.UpperPrice = float.Parse(data[3]);
                    item.Unit = data[4];
                    item.UpdateTime = DateTime.Parse(data[5]);
                    vm.PriceDetail.Add(item);
                }

                result.Add(vm);
            }

            return result;
        }


    }
}
