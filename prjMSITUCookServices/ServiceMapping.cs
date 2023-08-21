//using AutoMapper;
using AutoMapper;
using prjMSITUCook.Service.Dto.Info;
using prjMSITUCookServices.Dto.Info;
using prjMSITUCookServices.Dto.ResultModel;
using prjMSITUCookServices.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace prjMSITUCook.Service
{
    public class ServiceMapping:Profile
    {
        //entity ->resultModel
        public ServiceMapping()
        {
            //entity ->resultModel
                this.CreateMap<Recipe食譜, RecipeEditResultModel>()
        .ForMember(x => x.RecipeId, y => y.MapFrom(o => o.Recipe食譜Pk))
        .ForMember(x => x.RecipeName, y => y.MapFrom(o => o.RecipeName食譜名稱))
        .ForMember(x => x.RecipeCover, y => y.MapFrom(o => o.RecipeCover))
        .ForMember(x => x.ShortDescription, y => y.MapFrom(o => o.ShortDescription簡短說明))
        .ForMember(x => x.CostMinutes, y => y.MapFrom(o => o.CostMinutes花費時間))
        .ForMember(x => x.Amount, y => y.MapFrom(o => o.Amount份量))
        .ForMember(x => x.FavoriteNumber, y => y.MapFrom(o => o.FavoriteNumber收藏數))
        .ForMember(x => x.Likes, y => y.MapFrom(o => o.Likes讚數))
        .ForMember(x => x.Views, y => y.MapFrom(o => o.Views瀏覽數))
        .ForMember(x => x.AuthorId, y => y.MapFrom(o => o.Author作者))
        .ForMember(x => x.PublishedTime, y => y.MapFrom(o => o.PublishedTime出版時間));

            this.CreateMap<Step步驟列表, StepEditResultModel>()
        .ForMember(x => x.StepPicture, y => y.MapFrom(o => o.StepDescriptionPicture步驟附圖))
        .ForMember(x => x.StepDescription, y => y.MapFrom(o => o.StepDescription步驟說明));

            this.CreateMap<Food食材, FoodEditResultModel>()
        .ForMember(x => x.FoodName, y => y.MapFrom(o => o.FoodName食材名稱))
        .ForMember(x => x.FoodAmount, y => y.MapFrom(o => o.FoodAmount食材數量));

            this.CreateMap<Recipe食譜, RecipeArticleResultModel>()
        .ForMember(x => x.RecipeId, y => y.MapFrom(o => o.Recipe食譜Pk))
        .ForMember(x => x.RecipeName, y => y.MapFrom(o => o.RecipeName食譜名稱))
        .ForMember(x => x.RecipeCover, y => y.MapFrom(o => o.RecipeCover))
        .ForMember(x => x.ShortDescription, y => y.MapFrom(o => o.ShortDescription簡短說明))
        .ForMember(x => x.CostMinutes, y => y.MapFrom(o => o.CostMinutes花費時間))
        .ForMember(x => x.Amount, y => y.MapFrom(o => o.Amount份量))
        .ForMember(x => x.FavoriteNumber, y => y.MapFrom(o => o.FavoriteNumber收藏數))
        .ForMember(x => x.Likes, y => y.MapFrom(o => o.Likes讚數))
        .ForMember(x => x.Views, y => y.MapFrom(o => o.Views瀏覽數))
        .ForMember(x => x.AuthorId, y => y.MapFrom(o => o.Author作者))
        .ForMember(x => x.PublishedTime, y => y.MapFrom(o => o.PublishedTime出版時間));

            this.CreateMap<Step步驟列表, RecipeStepResultModel>()
        .ForMember(x => x.StepPicture, y => y.MapFrom(o => o.StepDescriptionPicture步驟附圖))
        .ForMember(x => x.StepDescription, y => y.MapFrom(o => o.StepDescription步驟說明));

            this.CreateMap<Food食材, RecipeFoodResultModel>()
        .ForMember(x => x.FoodName, y => y.MapFrom(o => o.FoodName食材名稱))
        .ForMember(x => x.FoodAmount, y => y.MapFrom(o => o.FoodAmount食材數量));

            this.CreateMap<Member會員, AuthorListItemResultModel>()
        .ForMember(x => x.MemberId, y => y.MapFrom(o => o.MemberId會員Pk))
        .ForMember(x => x.ProfilePhoto, y => y.MapFrom(o => o.ProfilePhoto頭貼))
        .ForMember(x => x.SelfIntro, y => y.MapFrom(o => o.SelfIntro自介))
        .ForMember(x => x.NickName, y => y.MapFrom(o => o.NickName暱稱));

            



        //create:info->entities
        this.CreateMap<RecipeCreateInfo, Recipe食譜>()
        .ForMember(x => x.Recipe食譜Pk, y => y.MapFrom(o => o.RecipeId))
        .ForMember(x => x.RecipeName食譜名稱, y => y.MapFrom(o => o.RecipeName))
        .ForMember(x => x.RecipeCover, y => y.MapFrom(o => o.RecipeCover))
        .ForMember(x => x.ShortDescription簡短說明, y => y.MapFrom(o => o.ShortDescription))
        .ForMember(x => x.CostMinutes花費時間, y => y.MapFrom(o => o.CostMinutes))
        .ForMember(x => x.Amount份量, y => y.MapFrom(o => o.Amount))
        .ForMember(x => x.FavoriteNumber收藏數, y => y.MapFrom(o => o.FavoriteNumber))
        .ForMember(x => x.Likes讚數, y => y.MapFrom(o => o.Likes))
        .ForMember(x => x.Views瀏覽數, y => y.MapFrom(o => o.Views))
        .ForMember(x => x.Author作者, y => y.MapFrom(o => o.AuthorId))
        .ForMember(x => x.PublishedTime出版時間, y => y.MapFrom(o => o.PublishedTime));

            this.CreateMap<StepCreateInfo, Step步驟列表>()
        .ForMember(x => x.StepDescriptionPicture步驟附圖, y => y.MapFrom(o => o.StepPicture))
        .ForMember(x => x.StepDescription步驟說明, y => y.MapFrom(o => o.StepDescription));

            this.CreateMap<FoodCreateInfo, Food食材>()
        .ForMember(x => x.FoodName食材名稱, y => y.MapFrom(o => o.FoodName))
        .ForMember(x => x.FoodAmount食材數量, y => y.MapFrom(o => o.FoodAmount));

            //notification: info ->entity
            this.CreateMap<NotificationInfo, NotificationRecord通知紀錄>()
                .ForMember(x => x.MemberId會員Fk, y => y.MapFrom(o => o.MemberId))
                .ForMember(x => x.NotificationType通知類型編號, y => y.MapFrom(o => o.Type))
                .ForMember(x => x.NotifyTime通知時間, y => y.MapFrom(o => o.NotifyTime))
                .ForMember(x => x.LinkedMemberId相關會員Fk, y => y.MapFrom(o => o.RelatedMemberId))
                .ForMember(x => x.LinkedRecipe相關食譜Fk, y => y.MapFrom(o => o.RelatedRecipeId))
                .ForMember(x => x.Readed已讀時間, y => y.MapFrom(o => o.ReadTime));

            
                 this.CreateMap<StepEditInfo, Step步驟列表>()
        .ForMember(x => x.StepDescriptionPicture步驟附圖, y => y.MapFrom(o => o.StepPicture))
        .ForMember(x => x.StepDescription步驟說明, y => y.MapFrom(o => o.StepDescription));

                this.CreateMap<FoodEditInfo, Food食材>()
        .ForMember(x => x.FoodName食材名稱, y => y.MapFrom(o => o.FoodName))
        .ForMember(x => x.FoodAmount食材數量, y => y.MapFrom(o => o.FoodAmount));

            this.CreateMap<AuthorSearchInfo, SearchAuthorResultModel>();
            this.CreateMap<RecipeSearchInfo, RecipeMemberResultModel>();
            this.CreateMap<RecipeSearchInfo, RecipeSearchResultModel>();
            

        }

    }
}
