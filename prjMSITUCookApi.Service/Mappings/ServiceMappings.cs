using AutoMapper;
using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Dtos.DataModel;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Mappings
{
    public class ServiceMappings:Profile
    {
        public ServiceMappings() {
            //follow
            // Info -> Condition
            this.CreateMap<FollowInfo, FollowCondition>();
            this.CreateMap<FollowSearchInfo, FollowSearchCondition>();
            this.CreateMap<FollowDeleteInfo, FollowDeleteCondition>();
            // DataModel -> ResultModel
            this.CreateMap<FollowDataModel, FollowResultModel>()
                .ForMember(x=>x.FollowId, y=>y.MapFrom(o =>o.FOLLOW_追蹤_ID))
                .ForMember(x =>x.WhoFollow,y=>y.MapFrom(o =>o.FOLLOWER_MEMBER_ID追蹤者_FK))
                .ForMember(x=>x.FollowWhom,y=>y.MapFrom(o=>o.FOLLOWED_MEMBER_ID被追蹤者_FK))
                .ForMember(x=> x.FollowTime,y=>y.MapFrom(o=>o.FOLLOW_TIME開始追蹤日期));

            //like
            //info -> condition
            this.CreateMap<LikeInfo, LikeCondition>();
            this.CreateMap<LikeDeleteInfo, LikeDeleteCondition>();

            //notification
            //info -> condition
            this.CreateMap<NotificationSearchInfo,NotificationSearchCondition>();
            this.CreateMap<NotificationDataModel, NotificationResultModel>()
                .ForMember(x=>x.MemberId, y =>y.MapFrom(o=>o.MEMBER_ID會員_FK))
                .ForMember(x=> x.ReadTime, y=>y.MapFrom(o=> o.READED_已讀時間))
                .ForMember(x=> x.NotificationId, y=>y.MapFrom(o =>o.NOTIFICATION_RECORD_通知紀錄_PK))
                .ForMember(x=> x.Type,y => y.MapFrom(o =>o.NOTIFICATION_TYPE通知類型編號))
                .ForMember(x=> x.NotificationTime, y=>y.MapFrom(o=>o.NOTIFY_TIME通知時間));
            this.CreateMap<RecipeDataModel, NotificationRelatedRecipeResultModel>()
                .ForMember(x =>x.Id,y=>y.MapFrom(o=>o.RECIPE食譜_PK))
                .ForMember(x=>x.AuthorName,y=>y.MapFrom(o=>o.NICK_NAME暱稱))
                .ForMember(x=> x.Name, y=>y.MapFrom(o=>o.RECIPE_NAME食譜名稱))
                .ForMember(x=>x.Cover,y=>y.MapFrom(o=>o.RECIPE_COVER))
                .ForMember(x=>x.AuthorProfilePicture,y=>y.MapFrom(o=>o.PROFILE_PHOTO頭貼))
                .ForMember(x=>x.Likes,y=>y.MapFrom(o=>o.LIKES_讚數))
                .ForMember(x=>x.PublishedTime,y=>y.MapFrom(o=>o.PUBLISHED_TIME出版時間));
            this.CreateMap<OrderDataModel, NotificationRelatedOrderResultModel>()
                .ForMember(x=>x.id,y=>y.MapFrom(o=>o.ORDER_NUMBER訂單號碼_PK))
                .ForMember(x=>x.ProductPicture,y=>y.MapFrom(o=>o.img))
                .ForMember(x=>x.MemberId,y=>y.MapFrom(o=>o.MEMBER_ID會員_FK));
            this.CreateMap<MemberDataModel, NotificationRelatedMemberResultModel>()
                .ForMember(x=>x.Nickname,y=>y.MapFrom(o=>o.NICK_NAME暱稱))
                .ForMember(x=>x.ProfilePicture,y=>y.MapFrom(o=>o.PROFILE_PHOTO頭貼))
                .ForMember(x=>x.Id,y=>y.MapFrom(o=>o.MEMBER_ID會員_PK));


        }
    }
}
