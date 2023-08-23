using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjMSITUCook.Models;
using prjMSITUCook.Models.Services;
using prjMSITUCook.Models.VM;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Net.Http;
using NuGet.Packaging;
using prjMSITUCookServices.EFModels;
using prjMSITUCookServices.Implement;
using prjMSITUCookServices.Interface;
using AutoMapper;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.Dto.Info;
using prjMSITUCook.Models.Parameter;
using prjMSITUCook.Service.Dto.Info;
using System.Reflection.Metadata;
using prjMSITUCook.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using prjMSITUCook.Models.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text;
//using AspNetCore;

namespace prjMSITUCook.Controllers
{
    public class RecipeController : Controller
    {
        private readonly MemberInfoService _memberInfoService;
        private readonly IHubContext<NotifyHub> _hubContext;
        private readonly IRecipeService _recipeService;
        private readonly IFollowService _followService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _host;
        private readonly int _pageSize = 10;
        public RecipeController(
                                IWebHostEnvironment host,
                                IRecipeService recipeService,
                                MemberInfoService memberInfoService,
                                IHubContext<NotifyHub> hubContext,
                                IFollowService followService)
        {
            _host = host;
            _recipeService = recipeService;
            _memberInfoService = memberInfoService;
            _followService = followService;
            _hubContext = hubContext;

            var config = new MapperConfiguration(cfg =>
             cfg.AddProfile<ControllerMapping>());

            this._mapper = config.CreateMapper();
        }
        public async Task<IActionResult> Article(int? id)
        {


            var resultModel = await _recipeService.GetRecipeArticle((int)id, _memberInfoService.MemberId);
    
            var vm = this._mapper.Map<RecipeArticleResultModel, RecipeArticleVM>(resultModel);
            vm.StepList = resultModel.StepList
                                .Select(x => this._mapper
                                                .Map<RecipeStepResultModel, RecipeArticleStepsVM>(x))
                                .ToList();
            vm.FoodList=resultModel.FoodList
                                .Select(x => this._mapper
                                                .Map<RecipeFoodResultModel, RecipeArticleFoodsVM>(x))
                                .ToList();


            return View(vm);
        }

        public IActionResult Member(RecipeSearchParameter parameter)
        {

            if (parameter.AuthorId == _memberInfoService.MemberId)
            {
                return RedirectToAction("MyPage");
            }

            //parameter -> info
            var info = this._mapper.Map<RecipeSearchParameter,RecipeSearchInfo>(parameter);

            //調資料
            var resultModel = _recipeService.GetRecipeMemberResultModel(info, _pageSize,_memberInfoService.MemberId);

            //resultModel -> VM:RecipeMemberVM、RecipeListItemVM
            RecipeMemberVM vm = this._mapper.Map<RecipeMemberResultModel, RecipeMemberVM>(resultModel);
            //找不到用戶
            if (vm == null)
            {
                return RedirectToAction("index", "home");
            }
            //轉list
            vm.ItemList = resultModel.ResultList
                                        .Select(x => this._mapper
                                        .Map<RecipeListItemResultModel
                                            , RecipeListItemVM>(x)).ToList();
            vm.PageCount = resultModel.ResultList.TotalPages;
            vm.HasNextPage = resultModel.ResultList.HasNextPage;
            vm.HasPreviousPage = resultModel .ResultList.HasPreviousPage;
            return View(vm);
        }
        [Authorize]
        public IActionResult MyPage(RecipeSearchParameter parameter)
        {


            //登入會員
            parameter.AuthorId = _memberInfoService.MemberId;
            var loginMember = _memberInfoService.MemberId;

            //parameter -> info
            var info = this._mapper.Map<RecipeSearchParameter, RecipeSearchInfo>(parameter);

            //調資料
            var resultModel = _recipeService
                                .GetRecipeMemberResultModel(info, _pageSize, loginMember);

            //resultModel -> VM:RecipeMemberVM、RecipeListItemVM
            RecipeMemberVM vm = this._mapper
                                    .Map<RecipeMemberResultModel, 
                                            RecipeMemberVM>(resultModel);
            //找不到用戶
            if (vm == null)
            {
                return RedirectToAction("index", "home");
            }
            //轉list
            vm.ItemList = resultModel.ResultList
                                    .Select(x => this._mapper
                                                .Map<RecipeListItemResultModel, 
                                                    RecipeListItemVM>(x)).ToList();
            vm.PageCount = resultModel.ResultList.TotalPages;
            vm.HasNextPage = resultModel.ResultList.HasNextPage;
            vm.HasPreviousPage = resultModel.ResultList.HasPreviousPage;

            return View(vm);


        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RecipeCreateParameter parameter)
        {

            parameter.AuthorId = _memberInfoService.MemberId;
            var info = this._mapper
                .Map<RecipeCreateParameter, RecipeCreateInfo>(parameter);
            info.StepList = parameter.StepList
                                    .Select(x=>this._mapper
                                    .Map<StepCreateParameter,StepCreateInfo>(x))
                                    .ToList();
            info.FoodList = parameter.FoodList
                                    .Select(x => this._mapper
                                    .Map<FoodCreateParameter, FoodCreateInfo>(x))
                                    .ToList();
            string path = _host.WebRootPath + "/Images/";
            
            bool isSuccess = _recipeService.CreateArticle(info,path);
            if (!isSuccess) {
                return StatusCode(500);
            }
            //通知追蹤我的且在線上的人
            List<string> followMeList = _followService
                                    .GetWhoFollowMe(_memberInfoService.MemberId)
                                    .Select(x => x.ToString()).ToList();
            var intersectResult = followMeList.Intersect(UserHandler.ConnectedIds).ToList();
            foreach (var i in intersectResult)
            {
                _hubContext.Clients.User(i).SendAsync("GetNewNotification");
            }

            return StatusCode(200);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {

            if (id == 0 || id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var recipeResultModel = _recipeService.GetRecipeEditResultModel((int)id);
            var recipeVM = this._mapper
                                .Map<RecipeEditResultModel
                                    , RecipeEditVM>(recipeResultModel);

            recipeVM.StepList = recipeResultModel
                                .StepList
                                .Select(x => this._mapper
                                .Map<StepEditResultModel, StepEditVM>(x))
                                .ToList();

            recipeVM.FoodList = recipeResultModel
                    .FoodList
                    .Select(x => this._mapper
                    .Map<FoodEditResultModel, FoodEditVM>(x))
                    .ToList();

            return View(recipeVM);
        }
        [HttpPut]
        public IActionResult Edit(RecipeEditParameter parameter)
        {

            parameter.AuthorId = _memberInfoService.MemberId;

            var info = this._mapper
                .Map<RecipeEditParameter, RecipeArticleEditInfo>(parameter);

            info.StepList = parameter.StepList
                            .Select(x=>this._mapper.Map<StepEditParameter,StepEditInfo>(x))
                            .ToList();
            info.FoodList = parameter.FoodList
                            .Select(x => this._mapper.Map<FoodEditParameter, FoodEditInfo>(x))
                            .ToList();
            string path = _host.WebRootPath + "/Images/";
            var isSuccess = _recipeService.EditArticle(info,path);
            
            if (isSuccess)
            {
                return StatusCode(200);
            }

            return StatusCode(500);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isSuccess = _recipeService.DeleteRecipe(id);
            if (isSuccess) {
                return StatusCode(200);
            }
            return StatusCode(500);
        }
        public IActionResult SearchResult(RecipeSearchParameter parameter)
        {


            //parameter -> info
            var info = this._mapper.Map<RecipeSearchParameter, RecipeSearchInfo>(parameter);

            //調資料
            var resultModel = _recipeService
                                .GetRecipeListResultModel(info, _pageSize);

            //resultModel -> VM:RecipeSearchResultVM:IRecipeListVM、RecipeListItemVM
            RecipeSearchResultVM vm = this._mapper
                                    .Map<RecipeSearchResultModel,
                                            RecipeSearchResultVM>(resultModel);
            //page轉
            vm.ItemList = resultModel.ResultList
                        .Select(x => this._mapper
                                    .Map<RecipeListItemResultModel,
                                        RecipeListItemVM>(x)).ToList();

            vm.PageCount = resultModel.ResultList.TotalPages;
            vm.HasNextPage = resultModel.ResultList.HasNextPage;
            vm.HasPreviousPage = resultModel.ResultList.HasPreviousPage;



            return View(vm);
        }

        public IActionResult SearchAuthorResult(AuthorSearchParameter parameter)
        {


            if (string.IsNullOrEmpty(parameter.AuthorName))
            {
                return RedirectToAction("Index", "Home");
            }
            var info = this._mapper.Map<AuthorSearchParameter, AuthorSearchInfo>(parameter);
            var resultModel = _recipeService.GetAuthorSearchResult(info,_memberInfoService.MemberId);
            var vm = this._mapper.Map<SearchAuthorResultModel, RecipeSearchAuthorResultVM>(resultModel);
            vm.AuthorSearchResult = resultModel.AuthorSearchResult
                                                .Select(x => this._mapper
                                                            .Map<AuthorListItemResultModel,
                                                                        AuthorListItemVM>(x))
                                                .ToList();

            return View(vm);
        }
        public IActionResult _RecipeListPartialView(RecipeSearchParameter parameter)
        {
            //parameter -> info
            var info = this._mapper.Map<RecipeSearchParameter, RecipeSearchInfo>(parameter);

            //調資料
            var resultModel = _recipeService
                                .GetRecipeListResultModel(info, _pageSize);

            //resultModel -> VM:RecipeSearchResultVM:IRecipeListVM、RecipeListItemVM
            RecipeSearchResultVM vm = this._mapper
                                    .Map<RecipeSearchResultModel,
                                            RecipeSearchResultVM>(resultModel);
            //page轉
            vm.ItemList = resultModel.ResultList
                        .Select(x => this._mapper
                                    .Map<RecipeListItemResultModel,
                                        RecipeListItemVM>(x)).ToList();
            vm.PageCount = resultModel.ResultList.TotalPages;
            vm.HasNextPage = resultModel.ResultList.HasNextPage;
            vm.HasPreviousPage = resultModel.ResultList.HasPreviousPage;

            return PartialView(vm);
        }
        public IActionResult _RecipeSearchInputPartialView()
        {
            return PartialView();
        }

        public IActionResult _AuthorListPartialView(AuthorSearchParameter parameter)
        {
            //var loginMemberId = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "MemberId").FirstOrDefault().Value);
            var info = this._mapper.Map<AuthorSearchParameter,
                                            AuthorSearchInfo>(parameter);
            var resultModel = _recipeService.GetAuthorSearchResult(info, _memberInfoService.MemberId);
            var vm = this._mapper.Map<SearchAuthorResultModel, 
                                            RecipeSearchAuthorResultVM>(resultModel);
            vm.AuthorSearchResult = resultModel.AuthorSearchResult
                                                .Select(x => this._mapper
                                                            .Map<AuthorListItemResultModel,
                                                                        AuthorListItemVM>(x))
                                                .ToList();



            return PartialView(vm);
        }


        public IActionResult _SmallRecipeListPartialView(int id) {
            var data = _recipeService.GetRecipeIFollow(id);
            if (data == null || data.Count() <= 0) {
                return PartialView();
            }
            var result = data.Select(x => this
                                ._mapper
                                .Map<RecipeIFollowResultModel
                                , RecipeSmallItem>(x))
                                .ToList();

            return PartialView(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("text/plain")]
        public async Task<IActionResult> GetShortIntroSuggest([FromBody] GetShortIntroSuggestParameter parameter) {
            HttpClient http = new HttpClient();

            IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            string bearer = Config.GetSection("ChatGPTAPIKey").Value;

            //authorization
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            //處理傳入的指示
            StringBuilder sb = new StringBuilder();

            int i = 1;
            foreach (var sentence in parameter.instruct) {
                sb.Append(i);
                sb.Append(sentence);
                sb.Append(" ");
                i++;
            }
            string instruct = sb.ToString();
            ChatRequestParameter body = new ChatRequestParameter() {
                model = "gpt-3.5-turbo",
                messages=new List<ChatRequestMessageParameter>() {
                    new ChatRequestMessageParameter(){
                        role="system",
                        content="你是一個文案大師。請依據使用者提供的內容，生出20~100字的食譜短簡介"
                    },
                    new ChatRequestMessageParameter(){
                        role="user",
                        content=instruct
                    }
                }
            };

            HttpResponseMessage response =await http.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", body);
            http.Dispose();

            string SuggestContent = "";
            if (response.IsSuccessStatusCode){
                ChatResponseParameter json= await response.Content.ReadFromJsonAsync<ChatResponseParameter>();
                SuggestContent = json.choices.ElementAt(0).message.content;
                return Content(SuggestContent);
            }
            Response.StatusCode = 500;
            return Content("失敗");
        }
    }
}
