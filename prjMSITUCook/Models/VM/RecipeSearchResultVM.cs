using prjMSITUCook.Models.Interface;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.Implement;

namespace prjMSITUCook.Models.VM
{
    public class RecipeSearchResultVM:IRecipeListVM
    {
        public string RecipeName { get; set; }
        public string Amount { get; set; }
        public string CostTime { get; set; }
        /// <summary>
        /// 以空白相隔食物名稱的字串
        /// </summary>
        public string FoodNames { get; set; }
        public string SortMode { get; set; }
        /// <summary>
        /// 當前頁數-若page為0則丟回全部資料
        /// </summary>
        public int PageIndex { get; set; }

        public int RecipeCount { get; set; }

        public int PageCount { get; set; }

        ///// <summary>
        ///// 食譜作者Id
        ///// </summary>
        public int? AuthorId { get; set; }

        /// <summary>
        /// 搜尋結果
        /// </summary>
        public List<RecipeListItemVM> ItemList { get; set; }
    }
}
