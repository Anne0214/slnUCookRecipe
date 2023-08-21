using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace prjMSITUCook.Models.Authorization
{
    public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        /// <summary>
        /// 自訂authorization中間件的行為，來達到自訂授權處理結果
        /// </summary>
        /// <param name="next"></param>
        /// <param name="context"></param>
        /// <param name="policy"></param>
        /// <param name="authorizeResult"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (!context.User.Identity.IsAuthenticated) {
                context.Response.Redirect("/home/login"); //跳轉網頁
                return;
            }
            if (!authorizeResult.Succeeded) {
                if (authorizeResult
            .AuthorizationFailure //授權失敗的原因
            .FailedRequirements //授權失敗原因的requirement們
            .OfType<MinimumArticleRequirement>() //篩選出這個IEnumerable裡面class為minimumArticleRequirement的成員
            .Any() //確認有東西嗎，回傳true表示授權失敗有因為不符合指定的requirement的關係
            )
                {
                        context.Response.Redirect("/home/VIP"); //跳轉網頁

                        return;
                }

            }
            await next(context);
        }
    }
}
