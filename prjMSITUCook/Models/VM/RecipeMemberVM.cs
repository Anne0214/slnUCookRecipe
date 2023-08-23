using prjMSITUCook.Models.Interface;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.Implement;

namespace prjMSITUCook.Models.VM
{
    public class RecipeMemberVM:IRecipeListVM
    {
        /// <summary>
        /// 排序方式
        /// </summary>
        public string SortMode { get; set; }

        /// <summary>
        /// 當前用戶有沒有追蹤作者
        /// </summary>  
        public bool AuthorIsFollowed { get; set; }

        public string NickName { get; set; }

        public string ShortIntro { get; set; }

        public int FanCount { get; set; }
        public int FollowCount { get; set; }
        public int RecipeCount { get; set; }
        public string ProfilePhoto { get; set; }

        public List<RecipeListItemVM> ItemList { get; set; }
        /// <summary>
        /// 當前頁數
        /// </summary>
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public string RecipeName { get; set; }
        public string Amount { get; set; }
        public string CostTime { get; set; }
        public string FoodNames { get; set; }
        public int? AuthorId { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

    }

}

