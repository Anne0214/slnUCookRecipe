using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class Follow追蹤
{
    public int Follow追蹤Id { get; set; }

    public int FollowerMemberId追蹤者Fk { get; set; }

    public int FollowedMemberId被追蹤者Fk { get; set; }

    public DateTime FollowTime開始追蹤日期 { get; set; }

    public virtual Member會員 FollowedMemberId被追蹤者FkNavigation { get; set; } = null!;

    public virtual Member會員 FollowerMemberId追蹤者FkNavigation { get; set; } = null!;
}
