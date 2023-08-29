using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using prjMSITUCook.Models;
using prjMSITUCook.Models.Interface;
using prjMSITUCook.Models.Services;
using prjMSITUCookServices.EFModels;
using prjMSITUCookServices.Implement;
using prjMSITUCookServices.Interface;
using static System.Collections.Specialized.BitVector32;

namespace prjMSITUCook.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly IFollowService _followRepo;
        private readonly IWebHostEnvironment _host;
        private readonly MemberInfoService _memberInfoService;
        public FollowController(IWebHostEnvironment host,
                                IFollowService followRepo,
                                MemberInfoService memberInfoService)
        {
            _host = host;
            _followRepo = followRepo;
            _memberInfoService = memberInfoService;
        }
        
        public IActionResult Index(string section)
        {


            if (string.IsNullOrEmpty(section)) { section = "content"; }
            ViewBag.Section = section;
            ViewBag.FollowCount = _followRepo
                                    .GetFollowerCount(_memberInfoService.MemberId);
            ViewBag.FanCount = _followRepo
                                    .GetFanCount(_memberInfoService.MemberId);

            return View();
        }



    }
}
