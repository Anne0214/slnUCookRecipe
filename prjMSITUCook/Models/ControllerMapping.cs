using AutoMapper;
using prjMSITUCook.Models.Parameter;
using prjMSITUCook.Models.VM;
using prjMSITUCook.Service.Dto.Info;
using prjMSITUCookServices.Dto.Info;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.EFModels;

namespace prjMSITUCook.Models
{
    public class ControllerMapping:Profile
    {
        public ControllerMapping() {
            //result ->VM
            this.CreateMap<RecipeArticleResultModel, RecipeArticleVM>();
            this.CreateMap<RecipeFoodResultModel, RecipeArticleFoodsVM>();
            this.CreateMap<RecipeStepResultModel, RecipeArticleStepsVM>();
            this.CreateMap<RecipeEditResultModel, RecipeEditVM>();
            this.CreateMap<FoodEditResultModel, FoodEditVM>();
            this.CreateMap<StepEditResultModel, StepEditVM>();
            this.CreateMap<AuthorListItemResultModel, AuthorListItemVM>();
            this.CreateMap<SearchAuthorResultModel, RecipeSearchAuthorResultVM>()
                .ForMember(x => x.AuthorSearchResult, y => y.Ignore());
            this.CreateMap<RecipeMemberResultModel, RecipeMemberVM>();
            this.CreateMap<RecipeSearchResultModel,RecipeSearchResultVM>();
            this.CreateMap<RecipeListItemResultModel, RecipeListItemVM>();
            this.CreateMap<RecipeMemberResultModel, RecipeMemberVM>()
                .ForMember(x => x.ItemList, y => y.Ignore());
            this.CreateMap<RecipeIFollowResultModel, RecipeSmallItem>();
            this.CreateMap<HomeRecipeResultModel, HomeRecipeVM>();
            this.CreateMap<FoodPriceDetailResultModel, FoodPriceEveryMarketVM>();

            

            //parameter -> info
            this.CreateMap<RecipeCreateParameter, RecipeCreateInfo>();
            this.CreateMap<StepCreateParameter, StepCreateInfo>();
            this.CreateMap<FoodCreateParameter, FoodCreateInfo>();
            this.CreateMap<RecipeEditParameter, RecipeArticleEditInfo>();
            this.CreateMap<FoodEditParameter, FoodEditInfo>();
            this.CreateMap<StepEditParameter, StepEditInfo>();
            this.CreateMap<AuthorSearchParameter, AuthorSearchInfo>();
            this.CreateMap<RecipeSearchParameter, RecipeSearchInfo>();
            this.CreateMap<RegisterParameter, MemberInfo>();
            this.CreateMap<LoginParameter, LoginInfo>();



        }
    }
}
