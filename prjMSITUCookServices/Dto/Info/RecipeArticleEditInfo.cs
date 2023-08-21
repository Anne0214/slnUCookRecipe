using Microsoft.AspNetCore.Http;
using prjMSITUCookServices.Dto.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCook.Service.Dto.Info
{
    public class RecipeArticleEditInfo
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

        public int Likes { get; set;  }

        public int FavoriteNumber { get; set; }
        public string Amount { get; set; }

        public List<FoodEditInfo> FoodList { get; set; }
        public List<StepEditInfo> StepList { get; set; }

    }
    public class StepEditInfo
    {
        public string StepPicture { get; set; }
        public IFormFile StepPictureFile { get; set; }
        public string StepDescription { get; set; }

    }
    public class FoodEditInfo
    {
        public string FoodName { get; set; }
        public string FoodAmount { get; set; }

    }
}
