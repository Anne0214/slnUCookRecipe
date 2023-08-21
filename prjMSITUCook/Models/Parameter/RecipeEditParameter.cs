using prjMSITUCook.Service.Dto.Info;
using System.ComponentModel.DataAnnotations;

namespace prjMSITUCook.Models.Parameter
{
    public class RecipeEditParameter
    {
        public int RecipeId { get; set; }

        public int AuthorId { get; set; }

        public string RecipeCover { get; set; }

        public IFormFile RecipeCoverFile { get; set; }

        public DateTime PublishedTime { get; set; }
        public string RecipeName { get; set; }
        public string CostMinutes { get; set; }

        public string ShortDescription { get; set; }

        public int Views { get; set; }

        public int Likes { get; set; }

        public int FavoriteNumber { get; set; }
        public string Amount { get; set; }
        public List<FoodEditParameter> FoodList { get; set; }
        public List<StepEditParameter> StepList { get; set; }

    }
    public class StepEditParameter
    {
        public string StepPicture { get; set; }
        public IFormFile StepPictureFile { get; set; }
        public string StepDescription { get; set; }

    }
    public class FoodEditParameter
    {
        public string FoodName { get; set; }
        public string FoodAmount { get; set; }

    }
}
