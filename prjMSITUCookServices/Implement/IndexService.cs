using Microsoft.EntityFrameworkCore;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.EFModels;
using prjMSITUCookServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace prjMSITUCookServices.Implement
{
    public class IndexService : IIndexService
    {
        private readonly UcookRecipeDatabaseContext _context;
        public IndexService(UcookRecipeDatabaseContext context) {
            _context = context;
        }
        public HomeIndexResultModel GetIndexContent()
        {
            var result = new HomeIndexResultModel();
            //取得最新食譜前5
            var LatestRecipe=_context.Recipe食譜s.OrderByDescending(x => x.PublishedTime出版時間)
                                .Include(z=>z.Author作者Navigation)
                                .Include(t=>t.LatestLikeLog最新按讚紀錄s)
                                .Take(5)
                                .Select(y=> new HomeRecipeResultModel {
                                    AuthorName = y.Author作者Navigation.NickName暱稱,
                                    AuthorPic = y.Author作者Navigation.ProfilePhoto頭貼,
                                    Likes =((int)y.Likes讚數)+(y.LatestLikeLog最新按讚紀錄s.Count()),
                                    PublishedTime = (DateTime)y.PublishedTime出版時間,
                                    RecipeCover =y.RecipeCover,
                                    RecipeId =y.Recipe食譜Pk,
                                    RecipeName = y.RecipeName食譜名稱
                                })
                                .ToList();

            result.LatestRecipes = LatestRecipe;





            return result;
        }
        public class SkuGroupItem {
            public int spu { get; set; }
            public int soldNumber { get; set; }
        }
    }
}
