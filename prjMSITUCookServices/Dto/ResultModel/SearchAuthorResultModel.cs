using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Dto.ResultModel
{
    public class SearchAuthorResultModel
    {
        public SearchAuthorResultModel() {
            AuthorSearchResult = new List<AuthorListItemResultModel>();
        }    
        /// <summary>
        /// 搜尋條件-姓名
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// 搜尋條件-誰追蹤的對象
        /// </summary>
        public int? FollowByWhom { get; set; }
        /// <summary>
        /// 搜尋條件-是誰的粉絲
        /// </summary>
        public int? WhoseFan { get; set; }
        /// <summary>
        /// 搜尋結果-數量
        /// </summary>
        public int AuthorCount { get; set; }
        /// <summary>
        /// 搜尋結果
        /// </summary>
        public List<AuthorListItemResultModel> AuthorSearchResult { get; set; }
    }
    public class AuthorListItemResultModel {
        public int MemberId { get; set; }
        public string NickName { get; set; }
        public string ProfilePhoto { get; set; }
        public string SelfIntro { get; set; }
        public int FanCount { get; set; }
        public int FollowCount { get; set; }
        public int RecipeCount { get; set; }
        /// <summary>
        /// 作者是否被當前登入用戶追蹤中
        /// </summary>
        public bool AuthorIsFollowed { get; set; }

    }
}
