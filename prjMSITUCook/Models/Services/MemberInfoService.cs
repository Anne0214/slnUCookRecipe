using Microsoft.Build.Evaluation;
using System.Security.Claims;

namespace prjMSITUCook.Models.Services
{
    public class MemberInfoService
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly List<Claim> ClaimList;
        
        public MemberInfoService(IHttpContextAccessor objHttpContextAccessor_Val)
        {
            _HttpContextAccessor = objHttpContextAccessor_Val;
            ClaimList = _HttpContextAccessor.HttpContext.User.Claims.ToList();

        }
        public bool IsLogin => ClaimList == null || ClaimList.Count <= 0 ? false : true;
        public string NickName => ClaimList==null || ClaimList.Count <= 0 ? null:ClaimList.FirstOrDefault(a => a.Type == "NickName").Value;
        public string Email => ClaimList == null || ClaimList.Count <= 0 ? null : ClaimList.FirstOrDefault(a => a.Type == "Email").Value;
        public int MemberId => ClaimList == null || ClaimList.Count <= 0 ? 0 : int.Parse(ClaimList.FirstOrDefault(a => a.Type == "MemberId").Value);
        public string ProfilePhoto => ClaimList == null || ClaimList.Count <= 0 ? null : ClaimList.FirstOrDefault(a => a.Type == "ProfilePhoto").Value;
        ///// <summary>
        ///// 取得已登入用戶的PK、暱稱及頭貼(ForLayout)
        ///// </summary>
        ///// <returns></returns>
        //public Dictionary<string,string> GetUserInfo()
        //{
        //    var ClaimList = _HttpContextAccessor.HttpContext.User.Claims.ToList();

        //    if (ClaimList == null) {
        //        return null;
        //    }
        //    var NickName = ClaimList.Where(a => a.Type == "NickName").First().Value;
        //    var Email = ClaimList.Where(a => a.Type == "Email").First().Value;
        //    var MemberId = ClaimList.Where(a => a.Type == "MemberId").First().Value;
        //    var ProfilePhoto = ClaimList.Where(a => a.Type == "ProfilePhoto").First().Value;

        //    Dictionary<string, string> info = new Dictionary<string, string>();
        //    info.Add("NickName",NickName);
        //    info.Add("Email",Email);
        //    info.Add("MemberId",MemberId);
        //    info.Add("ProfilePhoto", ProfilePhoto);

        //    return info;
        //}
    }
}
