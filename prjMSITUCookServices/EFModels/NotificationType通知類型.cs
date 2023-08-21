using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class NotificationType通知類型
{
    public int NotificationType通知類型編號Pk { get; set; }

    public string NotificationTypeName通知類型名稱 { get; set; } = null!;

    public string NotificationTrigger通知時機 { get; set; } = null!;

    public virtual ICollection<NotificationRecord通知紀錄> NotificationRecord通知紀錄s { get; set; } = new List<NotificationRecord通知紀錄>();
}
