using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class Member會員
{
    public int MemberId會員Pk { get; set; }

    public string? MemberEmail { get; set; }

    public string? MemberPassword { get; set; }

    public DateTime? RegisterTime註冊時間 { get; set; }

    public string? Gender性別 { get; set; }

    public DateTime? Birthday生日 { get; set; }

    public string? NickName暱稱 { get; set; }

    public string? ProfilePhoto頭貼 { get; set; }

    public string? SelfIntro自介 { get; set; }

    public string? ReceivedPersonName收件人姓名 { get; set; }

    public string? ReceivedPersonPhone收件人電話 { get; set; }

    public string? ReceivedPersonAddress收件人地址 { get; set; }

    public virtual ICollection<Follow追蹤> Follow追蹤FollowedMemberId被追蹤者FkNavigations { get; set; } = new List<Follow追蹤>();

    public virtual ICollection<Follow追蹤> Follow追蹤FollowerMemberId追蹤者FkNavigations { get; set; } = new List<Follow追蹤>();

    public virtual ICollection<LatestLikeLog最新按讚紀錄> LatestLikeLog最新按讚紀錄s { get; set; } = new List<LatestLikeLog最新按讚紀錄>();

    public virtual ICollection<MemberVerifyCode會員註冊驗證> MemberVerifyCode會員註冊驗證s { get; set; } = new List<MemberVerifyCode會員註冊驗證>();

    public virtual ICollection<NotificationRecord通知紀錄> NotificationRecord通知紀錄LinkedMemberId相關會員FkNavigations { get; set; } = new List<NotificationRecord通知紀錄>();

    public virtual ICollection<NotificationRecord通知紀錄> NotificationRecord通知紀錄MemberId會員FkNavigations { get; set; } = new List<NotificationRecord通知紀錄>();

    public virtual ICollection<PasswordSalt密碼鹽> PasswordSalt密碼鹽s { get; set; } = new List<PasswordSalt密碼鹽>();

    public virtual ICollection<Recipe食譜> Recipe食譜s { get; set; } = new List<Recipe食譜>();
}
