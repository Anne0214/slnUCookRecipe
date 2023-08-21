using System.ComponentModel.DataAnnotations;

namespace prjMSITUCook.Models.Parameter
{
    public class AuthorSearchParameter
    {
        /// <summary>
        /// 搜尋名字
        /// </summary>
        
        public string AuthorName { get; set; }
        /// <summary>
        /// 搜出來的作者都是誰追蹤的
        /// </summary>
        public int? FollowByWhom { get; set; }
        /// <summary>
        /// 搜出來的都是誰的粉絲
        /// </summary>
        public int? WhoseFan { get; set; }

    }
}
