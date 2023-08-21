using System;
using System.Collections.Generic;

namespace prjMSITUCookServices.EFModels;

public partial class Food食材
{
    public int Food食材Id { get; set; }

    public int? RecipeFood食譜Fk { get; set; }

    public string? FoodName食材名稱 { get; set; }

    public string? FoodAmount食材數量 { get; set; }

    public virtual Recipe食譜? RecipeFood食譜FkNavigation { get; set; }
}
