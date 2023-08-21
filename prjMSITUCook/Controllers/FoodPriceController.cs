using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjMSITUCook.Controllers
{
    public class FoodPriceController : Controller
    {

        [Authorize(Policy = "AtLeastPost3Article")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
