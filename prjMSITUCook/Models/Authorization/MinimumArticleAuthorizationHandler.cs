using Microsoft.AspNetCore.Authorization;
using prjMSITUCookServices.EFModels;

namespace prjMSITUCook.Models.Authorization
{
    public class MinimumArticleAuthorizationHandler : AuthorizationHandler<MinimumArticleRequirement>
    {
        private readonly UcookRecipeDatabaseContext _context;
        public MinimumArticleAuthorizationHandler(UcookRecipeDatabaseContext context) {
            _context = context;
        }

        /// <summary>
        /// 處理發文限制的handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumArticleRequirement requirement)
        {
            try
            {
                if (context.User.Claims.FirstOrDefault(y => y.Type == "MemberId") == null) {
                    return Task.CompletedTask;
                }

                var id = int.Parse(context.User.Claims.FirstOrDefault(y => y.Type == "MemberId").Value);
                //取得用戶發文數
                int count = _context.Recipe食譜s.Where(x => x.Author作者 == id).Count();
                //確認是否有到達require規定的限制，有就通過
                if (count >= requirement.Count)
                {
                    context.Succeed(requirement);
                }
            }
            catch {
                return Task.CompletedTask;
                
            }
            //該項不通過，但如果有後面的其他條件通過仍可得到授權
            return Task.CompletedTask;
        }
    }
}
