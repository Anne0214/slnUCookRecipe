using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class LatestLikeLog最新按讚紀錄
{
    public int LatestLikeLog最新按讚紀錄Pk { get; set; }

    public int MemberId會員Fk { get; set; }

    public int LikedRecipe按讚食譜Fk { get; set; }

    public DateTime LikedTime按讚時間 { get; set; }

    public virtual Recipe食譜 LikedRecipe按讚食譜FkNavigation { get; set; } = null!;

    public virtual Member會員 MemberId會員FkNavigation { get; set; } = null!;
}
