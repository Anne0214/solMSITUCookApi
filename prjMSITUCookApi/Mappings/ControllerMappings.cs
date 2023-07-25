using AutoMapper;
using prjMSITUCookApi.Models.Parameter;
using prjMSITUCookApi.Models.ViewModel;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;

namespace prjMSITUCookApi.Mappings
{
    public class ControllerMappings: Profile
    {
        public ControllerMappings() {
            this.CreateMap<FollowSearchParameter,FollowSearchInfo>();
            this.CreateMap<FollowParameter,FollowInfo>();
            this.CreateMap<FollowDeleteParameter,FollowDeleteInfo>();

            this.CreateMap<FollowResultModel, FollowViewModel>();

            //like
            //parameter -> info
            this.CreateMap<LikeParameter, LikeInfo>();
            this.CreateMap<LikeDeleteParameter, LikeDeleteInfo>();

            //notification
            //parameter ->info
            this.CreateMap<NotificationSearchParameter, NotificationSearchInfo>();
            this.CreateMap<NotificationRelatedMemberResultModel, NotificationRelatedMemberViewModel>();
            this.CreateMap<NotificationRelatedOrderResultModel, NotificationRelatedOrderViewModel>();
            this.CreateMap<NotificationRelatedRecipeResultModel, NotificationRelatedRecipeViewModel>();
            this.CreateMap<NotificationResultModel, NotificationViewModel>();


        }
    }
}
