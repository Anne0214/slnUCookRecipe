using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class MemberVerifyCode會員註冊驗證
{
    public int MemberVerifyCodeId { get; set; }

    public int MemberId會員Fk { get; set; }

    public string VerifyCode { get; set; } = null!;

    public DateTime VerityExpirationData過期日 { get; set; }

    public virtual Member會員 MemberId會員FkNavigation { get; set; } = null!;
}
