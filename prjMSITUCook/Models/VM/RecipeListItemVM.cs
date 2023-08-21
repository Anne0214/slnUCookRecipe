namespace prjMSITUCook.Models.VM
{
    public class RecipeListItemVM
    {
        public int RecipeId { get; set; }

        public string RecipeCover { get; set; }

        public string NickName { get; set; }

        public string ProfilePhoto { get; set; }

        public DateTime PublishedTime { get; set; }

        public string RecipeName { get; set; }

        public string ShortDescription { get; set; }

        public int Likes { get; set; }

        public int FavoriteNumber { get; set; }

        public int Views { get; set; }

        public List<string> FoodList { get; set; }
    }
}
