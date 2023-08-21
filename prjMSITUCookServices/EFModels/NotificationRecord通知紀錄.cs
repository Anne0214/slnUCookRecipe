using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class NotificationRecord通知紀錄
{
    public int NotificationRecord通知紀錄Pk { get; set; }

    public int MemberId會員Fk { get; set; }

    public DateTime NotifyTime通知時間 { get; set; }

    public DateTime? Readed已讀時間 { get; set; }

    public int NotificationType通知類型編號 { get; set; }

    public int? LinkedRecipe相關食譜Fk { get; set; }

    public int? LinkedMemberId相關會員Fk { get; set; }

    public virtual Member會員? LinkedMemberId相關會員FkNavigation { get; set; }

    public virtual Recipe食譜? LinkedRecipe相關食譜FkNavigation { get; set; }

    public virtual Member會員 MemberId會員FkNavigation { get; set; } = null!;

    public virtual NotificationType通知類型 NotificationType通知類型編號Navigation { get; set; } = null!;
}
