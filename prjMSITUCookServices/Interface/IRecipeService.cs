using prjMSITUCook.Service.Dto.Info;
using prjMSITUCookServices.Dto.Info;
using prjMSITUCookServices.Dto.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Interface
{
    public interface IRecipeService
    {
        /// <summary>
        /// 取得符合條件的詳細食譜資訊
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        RecipeSearchResultModel GetRecipeListResultModel(RecipeSearchInfo info, int pageSize);
        /// <summary>
        /// 取得要修改的食譜的資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RecipeEditResultModel GetRecipeEditResultModel(int id);
        /// <summary>
        /// 取得個人頁所需資料
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        RecipeMemberResultModel GetRecipeMemberResultModel(RecipeSearchInfo info, int pageSize, int id);

        //取得作者搜尋解果
        SearchAuthorResultModel GetAuthorSearchResult(AuthorSearchInfo info, int id);

        //取得食譜文章
        Task<RecipeArticleResultModel> GetRecipeArticle(int id, int memberId);

        //創建食譜
        bool CreateArticle(RecipeCreateInfo info, string path);

        //修改食譜
        bool EditArticle(RecipeArticleEditInfo info, string path);

        //刪除食譜
        bool DeleteRecipe(int id);
        //取得我追蹤的人的食譜
        List<RecipeIFollowResultModel> GetRecipeIFollow(int id);

    }
}
