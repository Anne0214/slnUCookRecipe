using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Dto.ResultModel
{
    public class HomeIndexResultModel
    {
        public List<HomeRecipeResultModel> LatestRecipes { get; set; }
    }
    public class HomeRecipeResultModel {
        public string RecipeName { get; set; }
        public string RecipeCover { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPic { get; set; }
        public DateTime PublishedTime { get; set; }
        public int Likes { get; set; }
        public int RecipeId { get; set; }
    }
}
