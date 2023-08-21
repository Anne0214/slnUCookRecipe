using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class Recipe食譜
{
    public int Recipe食譜Pk { get; set; }

    public int? Author作者 { get; set; }

    public string? RecipeCover { get; set; }

    public DateTime? PublishedTime出版時間 { get; set; }

    public string? RecipeName食譜名稱 { get; set; }

    public string? CostMinutes花費時間 { get; set; }

    public string? ShortDescription簡短說明 { get; set; }

    public int? Likes讚數 { get; set; }

    public int? FavoriteNumber收藏數 { get; set; }

    public int? Views瀏覽數 { get; set; }

    public string? Amount份量 { get; set; }

    public virtual Member會員? Author作者Navigation { get; set; }

    public virtual ICollection<Food食材> Food食材s { get; set; } = new List<Food食材>();

    public virtual ICollection<LatestLikeLog最新按讚紀錄> LatestLikeLog最新按讚紀錄s { get; set; } = new List<LatestLikeLog最新按讚紀錄>();

    public virtual ICollection<NotificationRecord通知紀錄> NotificationRecord通知紀錄s { get; set; } = new List<NotificationRecord通知紀錄>();

    public virtual ICollection<Step步驟列表> Step步驟列表s { get; set; } = new List<Step步驟列表>();
}
