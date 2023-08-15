using AutoMapper;
using prjMSITUCookApi.Models.Parameter;
using prjMSITUCookApi.Models.ViewModel;
using prjMSITUCookApi.Repository.Dtos.DataModel;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;

namespace prjMSITUCookApi.Mappings
{
    public class ControllerMappings : Profile
    {
        public ControllerMappings()
        {
            this.CreateMap<FollowSearchParameter, FollowSearchInfo>();
            this.CreateMap<FollowParameter, FollowInfo>();
            this.CreateMap<FollowDeleteParameter, FollowDeleteInfo>();

            this.CreateMap<FollowResultModel, FollowViewModel>();

            //like
            //parameter -> info
            this.CreateMap<LikeParameter, LikeInfo>();
            this.CreateMap<LikeDeleteParameter, LikeDeleteInfo>();

            //notification
            //parameter ->info
            this.CreateMap<NotificationSearchParameter, NotificationSearchInfo>();
            //resultModel ->viewModel
            this.CreateMap<NotificationRelatedMemberResultModel, NotificationRelatedMemberViewModel>();
            this.CreateMap<NotificationRelatedOrderResultModel, NotificationRelatedOrderViewModel>();
            this.CreateMap<NotificationRelatedRecipeResultModel, NotificationRelatedRecipeViewModel>();
            this.CreateMap<NotificationResultModel, NotificationViewModel>();

            //shoppingCart
            this.CreateMap<ShoppingCartPostParameter, ShoppingCartPostInfo>();
            this.CreateMap<ShoppingCartDataModel, ShoppingCartResultModel>()
            .ForMember(x => x.SkuId, y => y.MapFrom(o => o.SkuId))
               .ForMember(x => x.SpuId, y => y.MapFrom(o => o.SpuId))
               .ForMember(x => x.MemberId, y => y.MapFrom(o => o.MemberId))
               .ForMember(x => x.Quantity, y => y.MapFrom(o => o.Quantity))
               .ForMember(x => x.SetupTime, y => y.MapFrom(o => o.SetupTime));



        }
    }
}
