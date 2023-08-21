using prjMSITUCookServices.Dto.ResultModel;

namespace prjMSITUCook.Models.VM
{
    public class RecipeEditVM
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

        public List<FoodEditVM> FoodList { get; set; }
        public List<StepEditVM> StepList { get; set; }

    }
    public class StepEditVM
    {
        public string StepPicture { get; set; }
        public IFormFile StepPictureFile { get; set; }
        public string StepDescription { get; set; }

    }
    public class FoodEditVM
    {
        public string FoodName { get; set; }
        public string FoodAmount { get; set; }
    }
}
