namespace prjMSITUCook.Models.VM
{
    public class HomeIndexVM
    {
        public List<HomeRecipeVM> LatestRecipes { get; set; }
        public List<HomeProductVM> HotProduct { get; set; }
    }
    public class HomeRecipeVM {
        public string RecipeName { get; set; }
        public string RecipeCover { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPic { get; set; }
        public DateTime PublishedTime { get; set; }
        public int Likes { get; set; }
        public int RecipeId { get; set; }
    }

    public class HomeProductVM {
        public string ProductName { get; set; }
        public string ProductCover { get; set; }

        public int SPU { get; set; }
        public int TagPrice { get; set; }
        public int SalePrice { get; set; }
        public int Count { get; set; }
    }
}
