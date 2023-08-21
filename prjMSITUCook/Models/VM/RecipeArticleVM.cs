using prjMSITUCookServices.Dto.ResultModel;
using System.ComponentModel.DataAnnotations;

namespace prjMSITUCook.Models.VM
{
    public class RecipeArticleVM
    {
        public int RecipeId { get; set; }

        public int AuthorId { get; set; }
        public bool AuthorIsFollowed { get; set; }
        public bool IsLiked { get; set; }
        public string NickName { get; set; }

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

        public List<RecipeArticleFoodsVM> FoodList { get; set; }
        public List<RecipeArticleStepsVM> StepList { get; set; }

    }
    public class RecipeArticleFoodsVM{
        public string FoodName { get; set; }
        public string FoodAmount { get; set; }
    }
    public class RecipeArticleStepsVM {
        public string StepPicture { get; set; }
        public string StepDescription { get; set; }
    }
}
