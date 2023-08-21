using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class Step步驟列表
{
    public int Step步驟Id { get; set; }

    public int? RecipeStep食譜Fk { get; set; }

    public string? StepDescriptionPicture步驟附圖 { get; set; }

    public string? StepDescription步驟說明 { get; set; }

    public virtual Recipe食譜? RecipeStep食譜FkNavigation { get; set; }
}
