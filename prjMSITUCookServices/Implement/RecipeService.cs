using AutoMapper;
using Microsoft.EntityFrameworkCore;
using prjMSITUCook.Service;
using prjMSITUCook.Service.Dto.Info;
using prjMSITUCookServices.Dto.Info;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.EFModels;
using prjMSITUCookServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace prjMSITUCookServices.Implement
{
    public class RecipeService : IRecipeService
    {
        private readonly UcookRecipeDatabaseContext _context;
        private readonly CommonService _commonService;
        private readonly IMapper _mapper;
        private int _pageSize = 10;
        private readonly FollowService _followRepo;

        public RecipeService(UcookRecipeDatabaseContext context)
        {
            _context = context;
            _commonService = new CommonService(context);

            var config = new MapperConfiguration(cfg =>
             cfg.AddProfile<ServiceMapping>());

            this._mapper = config.CreateMapper();
            _followRepo = new FollowService(_context);

        }

        bool IRecipeService.CreateArticle(RecipeCreateInfo info, string path)
        {
            try
            {
                //加上發布時間
                info.PublishedTime = DateTime.Now;

                //圖片上傳，取回網址，輸入進info

                if (info.RecipeCoverFile != null)
                {
                    info.RecipeCover = _commonService.GetImgUrl(info.RecipeCoverFile, path).Result;
                }


                foreach (var i in info.StepList)
                { //foreach每個步驟
                  //要處理: 只要有檔案，無論有無StepPicture都要處理
                    if (i.StepPictureFile != null)
                    {
                        //用檔案得到網址，再傳給該步驟的steppicture參數
                        i.StepPicture = _commonService.GetImgUrl(i.StepPictureFile, path).Result;
                    }
                }
                //主體轉為entity物件(map)
                var result = this._mapper.Map<RecipeCreateInfo, Recipe食譜>(info);

                //步驟&食材轉為entity物件(map)
                var stepList = info.StepList.Select(x => this._mapper.Map<StepCreateInfo, Step步驟列表>(x));
                var foodList = info.FoodList.Select(x => this._mapper.Map<FoodCreateInfo, Food食材>(x));

                //合成entity物件
                result.Step步驟列表s = stepList.ToList();
                result.Food食材s = foodList.ToList();

                //寫入資料庫
                _context.Add(result);
                _context.SaveChanges();

                //寫入資料庫後增加通知

                //通知所有有追蹤該作者的人
                //有追蹤我的人清單，foreach
                List<int> followMe = _followRepo.GetWhoFollowMe((int)result.Author作者);
                foreach (int i in followMe) {
                    var notificationInfo = new NotificationInfo()
                    {
                        MemberId = i,
                        NotifyTime = DateTime.Now,
                        ReadTime = null,
                        Type = 1,
                        RelatedMemberId = null,
                        RelatedOrderId = null,
                        RelatedRecipeId = result.Recipe食譜Pk,
                    };

                    var notification = this._mapper.Map<NotificationInfo, NotificationRecord通知紀錄>(notificationInfo);

                    _context.Add(notification);

                }
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        bool IRecipeService.DeleteRecipe(int id)
        {
            try
            {
                var recipe = _context.Recipe食譜s.Find(id);
                _context.Remove(recipe);
                _context.SaveChanges();
            }
            catch { return false; }
            return true;

        }

        RecipeEditResultModel IRecipeService.GetRecipeEditResultModel(int id) {

            //找到食譜，匯入食材、步驟，轉成resultModel
            var recipeEntity = _context.Recipe食譜s.Include(x => x.Step步驟列表s)
                                            .Include(y => y.Food食材s)
                                            .FirstOrDefault(z=>z.Recipe食譜Pk==id);
            var recipeResultModel = this._mapper.Map<Recipe食譜,RecipeEditResultModel> (recipeEntity);

            var stepListResultModel = recipeEntity.Step步驟列表s
                                                .Select(x => this._mapper
                                                .Map<Step步驟列表, StepEditResultModel>(x));
            var foodListResultModel = recipeEntity.Food食材s
                                    .Select(x => this._mapper
                                    .Map<Food食材, FoodEditResultModel>(x));

            //return resultModel
            recipeResultModel.StepList = stepListResultModel.ToList();
            recipeResultModel.FoodList = foodListResultModel.ToList();


            return recipeResultModel;
        }
    
        
        bool IRecipeService.EditArticle(RecipeArticleEditInfo info,string path)
        {
            
            
            //圖片上傳，取回網址，輸入進info(同步)
            if (info.RecipeCoverFile != null)
            {
                info.RecipeCover = _commonService.GetImgUrl(info.RecipeCoverFile,path).Result;
            }
            foreach (var i in info.StepList)
            { //foreach每個步驟
                //要處理: 只要有檔案，無論有無StepPicture都要處理
                if (i.StepPictureFile != null)
                {
                    //用檔案得到網址，再傳給該步驟的steppicture參數
                    i.StepPicture =_commonService.GetImgUrl(i.StepPictureFile,path).Result;
                }
            }
            
            //找到要修改的食譜
            var recipe = _context.Recipe食譜s.Find(info.RecipeId);
            _context.Entry(recipe).Collection(x => x.Food食材s).Load();
            _context.Entry(recipe).Collection(y => y.Step步驟列表s).Load();
            

            //修改食譜的屬性
            recipe.CostMinutes花費時間 = info.CostMinutes;
            recipe.Amount份量 = info.Amount;
            recipe.RecipeName食譜名稱 = info.RecipeName;
            recipe.RecipeCover = info.RecipeCover;
            recipe.ShortDescription簡短說明 = info.ShortDescription;


            //刪除原本食譜的步驟&食材
            var delete_steps = _context.Step步驟列表s.Where(y => y.RecipeStep食譜Fk == info.RecipeId);
            _context.RemoveRange(delete_steps);

            var food_steps = _context.Food食材s.Where(y => y.RecipeFood食譜Fk == info.RecipeId);
            _context.RemoveRange(food_steps);

            //step & food轉成entity
            var stepEntity = info.StepList
                                .Select(x=>this._mapper
                                              .Map<StepEditInfo, Step步驟列表>(x))
                                .ToList();
            foreach (var i in stepEntity) { recipe.Step步驟列表s.Add(i); }

            var foodEntity = info.FoodList
                                .Select(x => this._mapper
                                              .Map<FoodEditInfo, Food食材>(x))
                                .ToList();
            foreach (var i in foodEntity) { recipe.Food食材s.Add(i); }

            //儲存
            _context.SaveChanges();

            return true;

        }

        SearchAuthorResultModel IRecipeService.GetAuthorSearchResult(AuthorSearchInfo info,int id)
        {
            var result = this._mapper.Map<AuthorSearchInfo, SearchAuthorResultModel>(info);
            
            //搜尋條件-名字
            if (!string.IsNullOrEmpty(info.AuthorName)) {
              var list = _context.Member會員s.Where(x => x.NickName暱稱.Contains(info.AuthorName));
                foreach (var m in list) {
                    var item = _commonService.TransferToAuthorListItem(m,id);
                    result.AuthorSearchResult.Add(item);
                }
            }
            //搜尋條件-是誰的粉絲們
            if (info.WhoseFan != null && info.WhoseFan != 0) {
                var fan = _context.Follow追蹤s.Where(x => x.FollowedMemberId被追蹤者Fk == info.WhoseFan).ToList();
                var list=fan.Select(x => _context.Member會員s.Find(x.FollowerMemberId追蹤者Fk)).ToList();
                foreach (var m in list)
                {
                    var item = _commonService.TransferToAuthorListItem(m,id);
                    result.AuthorSearchResult.Add(item);
                }
            }
            //搜尋條件-是誰的追蹤們
            if (info.FollowByWhom != null && info.FollowByWhom != 0)
            {
                var fan = _context.Follow追蹤s.Where(x => x.FollowerMemberId追蹤者Fk == info.FollowByWhom).ToList();
                var list = fan.Select(x => _context.Member會員s.Find(x.FollowedMemberId被追蹤者Fk)).ToList();
                foreach (var m in list)
                {
                    var item = _commonService.TransferToAuthorListItem(m,id);
                    result.AuthorSearchResult.Add(item);
                }
            }
            result.AuthorCount = result.AuthorSearchResult.Count;

            return result;
        }



        async Task<RecipeArticleResultModel> IRecipeService.GetRecipeArticle(int id,int memberId)
        {

            //取得食譜資料，如果食譜不存在return null
            var recipe = _context.Recipe食譜s
                            .Include(y=>y.Step步驟列表s)
                            .Include(z=>z.Food食材s)
                            .Include(g=>g.LatestLikeLog最新按讚紀錄s)
                            .FirstOrDefault(x => x.Recipe食譜Pk == id);
            //取得該食譜在按讚table的資料
            int plusLike = recipe.LatestLikeLog最新按讚紀錄s.Count();
            if (recipe == null)
            {
                return null;
            }
            //取得用戶資料(同步)
            var authorTask = _context.Member會員s.FirstOrDefaultAsync(x => x.MemberId會員Pk == recipe.Author作者);

            //entiy -> resultModel
            var recipeResultModel = this._mapper.Map<Recipe食譜, RecipeArticleResultModel>(recipe);

            //等author找到後要做的事
            var author = await authorTask;

            //確認用戶是否有追蹤該作者(同步)
            Task<bool> AuthorIsFollowedTask = _context.Follow追蹤s.AnyAsync(x => x.FollowerMemberId追蹤者Fk == memberId && x.FollowedMemberId被追蹤者Fk == recipe.Author作者);

            //author資料
            recipeResultModel.NickName = author.NickName暱稱;
            recipeResultModel.SelfIntro = author.SelfIntro自介;
            recipeResultModel.ProfilePhoto = author.ProfilePhoto頭貼;

            //like數加上去
            recipeResultModel.Likes += plusLike;

            //找到否追蹤後要做的事
            recipeResultModel.AuthorIsFollowed =await AuthorIsFollowedTask;

            //確認用戶是否有like該文章(同步)
            Task<bool> IsLikedTask = _context.LatestLikeLog最新按讚紀錄s.AnyAsync(x => x.MemberId會員Fk == memberId
                                                                     && x.LikedRecipe按讚食譜Fk == id);
            //取得步驟資料，並轉成ResultModel
            var stepEntityList = recipe.Step步驟列表s.ToList() ;
            var stepList = stepEntityList.Select(x => this._mapper.Map<Step步驟列表, RecipeStepResultModel>(x));

            //取得食材資料，並轉成ResultModel
            var foodEntityList = recipe.Food食材s.ToList();
            var foodList = foodEntityList.Select(x => this._mapper.Map<Food食材, RecipeFoodResultModel>(x));

            //找到追蹤與否喜歡與否後要做的事
            recipeResultModel.IsLiked =await IsLikedTask;

            //組成RecipeArticleResultModel
            //塞food、step
            recipeResultModel.StepList = stepList.ToList();
            recipeResultModel.FoodList = foodList.ToList();

            return recipeResultModel;

        }


        RecipeSearchResultModel IRecipeService.GetRecipeListResultModel(RecipeSearchInfo info,int pageSize)
        {
            IQueryable<Recipe食譜> data = _context.Recipe食譜s.AsQueryable<Recipe食譜>();
            //搜尋條件

            if (!string.IsNullOrEmpty(info.RecipeName))
            {
                data = data.Where(x => x.RecipeName食譜名稱.Contains(info.RecipeName));
            }
            //info.Amount = (info.Amount == "all") ? "" : info.Amount;
            //info.CostTime = (info.CostTime == "all") ? "" : info.CostTime;
            if (!string.IsNullOrEmpty(info.Amount) && info.Amount != "all")
            {
                data = data.Where(x => x.Amount份量 == info.Amount);
            }
            if (!string.IsNullOrEmpty(info.CostTime) && info.CostTime !="all")
            {   
                data = data.Where(x => x.CostMinutes花費時間 == info.CostTime);
            }
            if (info.AuthorId != null)
            {
                data = data.Where(x => x.Author作者 == info.AuthorId);
            }
            RecipeSearchResultModel result = this._mapper.Map<RecipeSearchInfo, RecipeSearchResultModel>(info);
            //排序

            //預設排序
            if (string.IsNullOrEmpty(info.SortMode))
            {
                result.SortMode = "LikeDesc";
            }

            switch (result.SortMode)
            {
                case "LikeDesc":
                    data = data.OrderByDescending(x => x.Likes讚數);
                    break;
                case "FavoriteDesc":
                    data = data.OrderByDescending(x => x.FavoriteNumber收藏數);
                    break;
                case "PublishedTimeDesc":
                    data = data.OrderByDescending(x => x.PublishedTime出版時間);
                    break;
                case "PublishedTimeAsc":
                    data = data.OrderBy(x => x.PublishedTime出版時間);
                    break;
            }

            var dataResult = data.ToList();

            //食材篩選
            //載入食譜的食材資料
            //query.ForEachAsync(q => _context.Entry(q).Collection(x => x.Food食材s).Load());
            if (!string.IsNullOrEmpty(info.FoodNames))
            {
                string[] food_names = info.FoodNames.Split(" "); //拆解每個食材變array

                foreach (string i in food_names) //一個個食材進行篩選 
                {
                    List<Recipe食譜> _filter = new List<Recipe食譜>(); //紀錄每個食材篩選結果

                    foreach (var r in dataResult) //針對上一輪的篩選再進行篩選
                    {

                        _context.Entry(r).Collection(x => x.Food食材s).Load(); //load這些食譜的food資料

                        if (r.Food食材s.Any(x => x.FoodName食材名稱 == i))
                        {
                            _filter.Add(r);
                        }

                    }
                    dataResult = _filter; //替換成新的篩選結果

                }
            }
            //轉成RecipeListItemResultModel的List
            List<RecipeListItemResultModel> resultList = new List<RecipeListItemResultModel>();
            foreach (var r in dataResult) {
                _context.Entry(r).Collection(x => x.Food食材s).Load();
                _context.Entry(r).Reference(x => x.Author作者Navigation).Load();
                _context.Entry(r).Collection(z => z.LatestLikeLog最新按讚紀錄s).Load();
                int plusLike = r.LatestLikeLog最新按讚紀錄s.Count();
                RecipeListItemResultModel item = new RecipeListItemResultModel() { 
                    RecipeCover = r.RecipeCover,
                    RecipeId = r.Recipe食譜Pk,
                    RecipeName = r.RecipeName食譜名稱,
                    FavoriteNumber =(int)r.FavoriteNumber收藏數,
                    Likes= ((int)r.Likes讚數) + plusLike,
                    NickName = r.Author作者Navigation.NickName暱稱,
                    ShortDescription = r.ShortDescription簡短說明,
                    ProfilePhoto = r.Author作者Navigation.ProfilePhoto頭貼,
                    PublishedTime =(DateTime) r.PublishedTime出版時間,
                    Views =(int)r.Views瀏覽數,
                    FoodList = r.Food食材s.Take(5).Select(x=>x.FoodName食材名稱).ToList(),
                };
                
                resultList.Add(item);
            }
            result.RecipeCount = resultList.Count();
            //resultList做pagination
            if (info.PageIndex == 0 || info.PageIndex == null)
            {
                result.PageIndex = 1;
            }
            else {
                result.PageIndex = info.PageIndex;
            }
            result.ResultList= PaginatedList<RecipeListItemResultModel>.CreateAsync(resultList, result.PageIndex, pageSize);

            return result;

        }
        List<RecipeIFollowResultModel> IRecipeService.GetRecipeIFollow(int id) {

            //取得該會員追蹤的清單
            var follows = _context.Follow追蹤s
                            .Where(x => x.FollowerMemberId追蹤者Fk == id)
                            .ToList();
            
            if (follows == null || follows.ToList().Count == 0)
            {
                return null;
            }
            //取得我追蹤的人的食譜，存在recipes
            List<Recipe食譜> recipes = new List<Recipe食譜>();
            
            foreach (var f in follows)
            {
                var recipe = _context.Recipe食譜s
                            .Where(x => x.Author作者 == f.FollowedMemberId被追蹤者Fk)
                            .ToList();
                recipes.AddRange(recipe);
            }
            if (recipes.Count <= 0)
            {
                return null;
            }
            //由新至舊排，只取前30個
            recipes = recipes.OrderByDescending(x => x.PublishedTime出版時間).Take(30).ToList();
            //轉成vm
            List<RecipeIFollowResultModel> result = recipes.Select(x => new RecipeIFollowResultModel()
            {
                RecipeCover = x.RecipeCover,
                RecipeId = x.Recipe食譜Pk,
                RecipeName = x.RecipeName食譜名稱,
                NickName = _context.Member會員s
                                .Where(y=>y.MemberId會員Pk==x.Author作者)
                                .FirstOrDefault()
                                .NickName暱稱
            }).ToList();

            return result;
        }

        RecipeMemberResultModel IRecipeService.GetRecipeMemberResultModel(RecipeSearchInfo info,int pageSize,int id)
        {
            if (info.AuthorId == null || info.AuthorId == 0) {

                return null;
            }
            //搜尋作者的食譜並排序
            var recipe = _context.Recipe食譜s.Where(x => x.Author作者 == info.AuthorId);
            var recipeCount = recipe.Count();
            if (string.IsNullOrEmpty(info.SortMode)) {
                info.SortMode = "LikeDesc";
            }
            switch (info.SortMode)
            {
                case "LikeDesc":
                    recipe = recipe.OrderByDescending(x => x.Likes讚數);
                    break;
                case "FavoriteDesc":
                    recipe = recipe.OrderByDescending(x => x.FavoriteNumber收藏數);
                    break;
                case "PublishedTimeDesc":
                    recipe = recipe.OrderByDescending(x => x.PublishedTime出版時間);
                    break;
                case "PublishedTimeAsc":
                    recipe = recipe.OrderBy(x => x.PublishedTime出版時間);
                    break;
            }
            //轉成ListItemResultModel
            var dataResult = recipe.ToList();

            //把return的東西塞一塞
            RecipeMemberResultModel result = this._mapper.Map<RecipeSearchInfo, RecipeMemberResultModel>(info);
            var author = _context.Member會員s.Find(info.AuthorId);
            _context.Entry(author).Collection(x => x.Follow追蹤FollowedMemberId被追蹤者FkNavigations).Load();
            result.AuthorIsFollowed =author.Follow追蹤FollowedMemberId被追蹤者FkNavigations.Any(x => x.FollowerMemberId追蹤者Fk == id);
            result.ShortIntro = author.SelfIntro自介;
            result.AuthorId =(int)info.AuthorId;
            result.NickName = author.NickName暱稱;
            result.ProfilePhoto = author.ProfilePhoto頭貼;
            result.FanCount = _followRepo.GetFanCount((int)info.AuthorId);
            result.FollowCount = _followRepo.GetFollowerCount((int)info.AuthorId);
            result.RecipeCount = recipeCount;
            result.SortMode = info.SortMode;
            List<RecipeListItemResultModel> resultList = new List<RecipeListItemResultModel>();
            foreach (var r in dataResult)
            {
                _context.Entry(r).Collection(x => x.Food食材s).Load();
                _context.Entry(r).Reference(x => x.Author作者Navigation).Load();
                _context.Entry(r).Collection(z =>z.LatestLikeLog最新按讚紀錄s).Load();

                int plusLike = r.LatestLikeLog最新按讚紀錄s.Count();
                RecipeListItemResultModel item = new RecipeListItemResultModel()
                {
                    RecipeCover = r.RecipeCover,
                    RecipeId = r.Recipe食譜Pk,
                    RecipeName = r.RecipeName食譜名稱,
                    FavoriteNumber = (int)r.FavoriteNumber收藏數,
                    Likes = ((int)r.Likes讚數)+ plusLike,
                    NickName = r.Author作者Navigation.NickName暱稱,
                    ShortDescription = r.ShortDescription簡短說明,
                    ProfilePhoto = r.Author作者Navigation.ProfilePhoto頭貼,
                    PublishedTime = (DateTime)r.PublishedTime出版時間,
                    Views = (int)r.Views瀏覽數,
                    FoodList = r.Food食材s.Take(5).Select(x => x.FoodName食材名稱).ToList(),
                };

                resultList.Add(item);
            }
            //做paginate
            if (info.PageIndex == 0 || info.PageIndex == null)
            {
                result.PageIndex = 1;
            }
            else {
                result.PageIndex = info.PageIndex;
            }
            result.ResultList = PaginatedList<RecipeListItemResultModel>.CreateAsync(resultList, result.PageIndex, pageSize);

            return result;

        }

    }

}
