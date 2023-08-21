using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using prjMSITUCook.Models;
using prjMSITUCook.Models.Coravel;
using prjMSITUCook.Models.VM;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.Interface;
using System.Text;

namespace prjMSITUCook.Controllers.Api
{
    [Route("api/FoodPrice/[action]")]
    [ApiController]
    public class FoodPriceApiController : ControllerBase
    {
        private readonly int _pageSize = 10;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        private readonly AgriProductService _agriProdService;


        public FoodPriceApiController(IDistributedCache cache) {

            _cache = cache;
            _agriProdService = new AgriProductService(_cache);

            var config = new MapperConfiguration(cfg =>
             cfg.AddProfile<ControllerMapping>());

            this._mapper = config.CreateMapper();
        }
        [HttpGet]
        [Produces("application/json")] //指定回傳格式是json
        [Route("{pageIndex}")]
        public List<FoodPriceVM> GetPaginatedFoodPrice([FromRoute] int pageIndex) {

            var cacheItem = _cache.GetString("AgriProductTrans");
            if (cacheItem == null)
            {
                Response.StatusCode = 500;
                return null;
            }
            else {
                var result = _agriProdService.DecomposeRadiusValuseForAgriProductTrans(cacheItem);
                int skip = (pageIndex-1)*_pageSize;
                return result.Skip(skip).Take(_pageSize).ToList();
            }
            


        }
    }
}
