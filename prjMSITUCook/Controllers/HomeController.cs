using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjMSITUCook.Models;
using prjMSITUCook.Models.Parameter;
using System.Diagnostics;
using System.Security.Claims;
using prjMSITUCookServices.EFModels;
using prjMSITUCook.Models.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using prjMSITUCookServices.Interface;
using AutoMapper;
using prjMSITUCookServices.Dto.Info;
using prjMSITUCookServices.Implement;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.ComponentModel;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCook.Models.VM;

namespace prjMSITUCook.Controllers
{
    public class HomeController : Controller
    {
        private readonly UcookRecipeDatabaseContext _context;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;
        private readonly IIndexService _indexService;

        public HomeController(UcookRecipeDatabaseContext context,
                                IMemberService memberService,
                                IIndexService indexService) {
            _context = context;
            _memberService = memberService;
            _indexService = indexService;
            var config = new MapperConfiguration(cfg =>
             cfg.AddProfile<ControllerMapping>());

            this._mapper = config.CreateMapper();

        }
        public IActionResult Index()
        {
            var resultModel = _indexService.GetIndexContent();

            HomeIndexVM vm = new HomeIndexVM();

            vm.LatestRecipes = resultModel.LatestRecipes
                                    .Select(x => this._mapper.Map<HomeRecipeResultModel, HomeRecipeVM>(x))
                                    .ToList();


            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginParameter parameter) {

            if (!ModelState.IsValid) {
                Response.StatusCode = 400;
                var errorList=ModelState.Values
                                        .SelectMany(x => x.Errors.Select(y=>y.ErrorMessage))
                                        .ToList();
                return Json(errorList);
            }
            var info = this._mapper.Map<LoginParameter, LoginInfo>(parameter);
            (var member,string error)=_memberService.VerifyLogin(info);

            if (member == null) {
                switch (error) {
                    case "不存在該筆帳號":
                        Response.StatusCode = 400;
                        return Content("帳密有錯");

                    case "尚未驗證完成":
                        return Content("尚未驗證完成");

                    case "註冊流程出錯，請重新註冊":
                        Response.StatusCode = 400;
                        return Content("帳密有錯");

                    case "密碼錯誤":
                        Response.StatusCode = 400;
                        return Content("帳密有錯");
                    case "發生未知錯誤":
                        Response.StatusCode = 500;
                        return Content("伺服器發生未知問題");

                }

            }

            //依據identity的要求，把對應的用戶資料放進claim類別格式
            var varClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,member.Email), //用戶帳號
                new Claim("NickName",member.NickName), //用戶暱稱(自訂名稱)
                new Claim(ClaimTypes.Email, member.Email),//用戶email
                new Claim("MemberId",member.MemberId.ToString()), //用戶PK
                new Claim(ClaimTypes.NameIdentifier,member.MemberId.ToString()),//用戶pk
                new Claim("ProfilePhoto",member.ProfilePhoto), //用戶頭貼
            };

            //建構ClaimsIdentity Cookie，也就是用戶驗證物件的狀態存取案例
            var varClaimsIdentity = new ClaimsIdentity(varClaims,
                                            CookieAuthenticationDefaults.AuthenticationScheme);
            //執行ClaimsIdentity Cookie 用戶驗證物件的操作登入動作(使用cookie操作內部驗證狀態控管與流程執行)
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(varClaimsIdentity));

            Response.StatusCode = 200;
            return Content("登入成功");
        }

        [HttpGet] //只能用Get
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");   
        }

        /// <summary>
        /// 未登入訪問登入功能處理
        /// </summary>
        /// <returns></returns>
        public IActionResult NoLogin()
        {
            return Content("尚未登入");
        }
        /// <summary>
        /// 未授權
        /// </summary>
        /// <returns></returns>
        public IActionResult NoRule()
        {
            return Content("未授權");
        }
        /// <summary>
        /// 註冊頁
        /// </summary>
        /// <returns></returns>
        public IActionResult Register() {
            return View();
        }

        /// <summary>
        /// 發動註冊，存到資料庫
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(RegisterParameter parameter) {

            if (!ModelState.IsValid) {
                return StatusCode(400);
            }

            //完成後轉導到SendVerificationLetter
            MemberInfo info = this._mapper.Map<RegisterParameter, MemberInfo>(parameter);
            string host = Request.Scheme + "://" + Request.Host;
            var memberId = _memberService.CreateNewMember(info, host);

            if (memberId == 1) {
                Response.StatusCode = 400;
                return Content("{ \"error\": \"該帳號已註冊\" }", "application/json");
            }

            if (memberId == 2)
            {
                Response.StatusCode = 500;
                return Content("{ \"error\": \"信箱有錯，無法寄出\" }", "application/json");
            }
            Response.StatusCode = 200;
            return Content($"/home/SendVerificationLetter/?email={parameter.Email}");
            //return RedirectToAction("SendVerificationLetter", "Home", new { email = parameter.Email});
        }
        /// <summary>
        /// 告知用戶已寄驗證信頁面,請於24小時內驗證
        /// </summary>
        /// <param name="id">該名註冊會員id</param>
        /// <returns></returns>

        public IActionResult SendVerificationLetter(string email) {
            ViewBag.Email = email;
            if (!_memberService.CheckVerifyingMember(email)) {
                return RedirectToAction("NotFound");
            }
            return View();
        }

        /// <summary>
        /// 驗證連結網址，成功則轉導success，查無則轉導fail
        /// </summary>
        /// <returns></returns>
        public IActionResult Verification(string code) {
            if (_memberService.VerifyRegisterEmail(code)) {
                return RedirectToAction("RegisterSuccess");
            }


            //驗證失敗就轉not found
            return RedirectToAction("NotFound");
        }
        /// <summary>
        /// 找不到該網址，e.g.註冊連結已失效
        /// </summary>
        /// <returns></returns>
        public IActionResult NotFound() {
            return View();
        }

        /// <summary>
        /// 註冊成功
        /// </summary>
        /// <returns></returns>
        public IActionResult RegisterSuccess() {
            return View();
        }


        public IActionResult CheckSameNickName(string name) {
            if (_memberService.CheckSameName(name)) {
                return Content("重複");
            }
            return Content("不重複");
        }

        public IActionResult CheckSameEmail(string email) {
            if (_memberService.CheckSameEmail(email))
            {
                return Content("重複");
            }
            return Content("不重複");

        }
        public IActionResult SearchIndex() {
            return View();
        }

        public IActionResult SendVerificationEmailAgain(string email) {
            string host = Request.Scheme + "://" + Request.Host;
            if (_memberService.SendVerificationLetterAgain(email,host))
            {

                return StatusCode(200);
            }
            else {
                return StatusCode(500);
            }

        }

        public IActionResult VIP() {
            return View();
        }

    }
}