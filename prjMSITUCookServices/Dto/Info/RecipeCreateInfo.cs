
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Dto.Info
{
    public class RecipeCreateInfo
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

        public List<FoodCreateInfo> FoodList { get; set; }
        public List<StepCreateInfo> StepList { get; set; }
    }
    public class StepCreateInfo
    {
        public string StepPicture { get; set; }
        public IFormFile StepPictureFile { get; set; }
        public string StepDescription { get; set; }

    }
    public class FoodCreateInfo
    {
        public string FoodName { get; set; }
        public string FoodAmount { get; set; }

    }


}

