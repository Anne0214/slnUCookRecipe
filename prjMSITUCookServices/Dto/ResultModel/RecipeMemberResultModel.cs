using prjMSITUCookServices.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Dto.ResultModel
{
    public class RecipeMemberResultModel
    {
        /// <summary>
        /// 排序方式
        /// </summary>
        public string SortMode { get; set; }
        /// <summary>
        /// 當前頁數
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 食譜作者Id
        /// </summary>
        public int AuthorId { get; set; }

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

        public PaginatedList<RecipeListItemResultModel> ResultList { get; set; }
    }
}
