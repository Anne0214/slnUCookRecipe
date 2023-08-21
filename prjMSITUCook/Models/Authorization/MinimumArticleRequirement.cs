using Microsoft.AspNetCore.Authorization;

namespace prjMSITUCook.Models.Authorization
{
    /// <summary>
    /// 限制最少要發幾篇文
    /// </summary>
    public class MinimumArticleRequirement: IAuthorizationRequirement
    {
        public MinimumArticleRequirement(int count) {
            Count = count;
        }
        public int Count { get; set; }
    }
}
