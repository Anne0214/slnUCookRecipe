using prjMSITUCook.Models.VM;
using prjMSITUCookServices.Implement;

namespace prjMSITUCook.Models.Interface
{
    public interface IRecipeListVM
    {
        string SortMode { get; set; }

        int PageIndex { get; set; }

        int PageCount { get; set; }

        List<RecipeListItemVM> ItemList { get; set; }

        string RecipeName { get; set; }
        string Amount { get; set; }
        string CostTime { get; set; }
        /// <summary>
        /// 以空白相隔食物名稱的字串
        /// </summary>
        string FoodNames { get; set; }

        /// <summary>
        /// 食譜作者Id
        /// </summary>
        int? AuthorId { get; set; }

        bool HasNextPage { get; set; }

        bool HasPreviousPage { get; set; }  
    }
}
