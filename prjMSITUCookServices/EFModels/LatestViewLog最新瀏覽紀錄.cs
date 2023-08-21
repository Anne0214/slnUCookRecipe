using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class LatestViewLog最新瀏覽紀錄
{
    public int LatestViewLog最新瀏覽紀錄Pk { get; set; }

    public int MemberId會員Fk { get; set; }

    public int ViewedRecipe瀏覽食譜Fk { get; set; }

    public DateTime ViewedTime瀏覽時間 { get; set; }

    public virtual Member會員 MemberId會員FkNavigation { get; set; } = null!;

    public virtual Recipe食譜 ViewedRecipe瀏覽食譜FkNavigation { get; set; } = null!;
}
