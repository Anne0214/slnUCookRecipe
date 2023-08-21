using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Dto.ResultModel
{
    public class RecipeArticleResultModel
    {
        public int RecipeId {get; set; }

        public int AuthorId { get; set; }
        public bool AuthorIsFollowed { get; set; }
        public bool IsLiked { get; set; }
        public string NickName{ get; set; }

        public string ProfilePhoto { get; set; }

        public string SelfIntro { get; set; }

        public string RecipeCover { get; set; }

        public DateTime PublishedTime { get; set; }

        public string RecipeName { get; set; }

        public string CostMinutes { get; set; }

        public string ShortDescription { get; set; }

        public int Likes { get; set; }

        public int FavoriteNumber { get; set; }

        public int Views { get; set; }

        public string Amount { get; set; }

        public List<RecipeFoodResultModel> FoodList { get; set; }
        public List<RecipeStepResultModel> StepList { get; set; }

    }
    public class RecipeFoodResultModel
    {
        public string FoodName { get; set; }
        public string FoodAmount { get; set; }

    }
    public class RecipeStepResultModel
    {
        public string StepPicture { get; set; }
        public string StepDescription { get; set; }

    }
}
