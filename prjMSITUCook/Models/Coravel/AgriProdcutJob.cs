using Coravel.Invocable;
using Microsoft.Extensions.Caching.Distributed;

namespace prjMSITUCook.Models.Coravel
{
    public class AgriProdcutJob : IInvocable
    {
        private IDistributedCache _cache;
        public AgriProdcutJob(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task Invoke()
        {
            var service = new AgriProductService(_cache);
            var result=await service.ImportToCache();
            return;
        }
    }
}
