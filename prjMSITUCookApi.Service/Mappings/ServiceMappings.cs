using AutoMapper;
using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Dtos.DataModel;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
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
            //info -> confition
            this.CreateMap<LikeInfo, LikeCondition>();
            this.CreateMap<LikeDeleteInfo, LikeDeleteCondition>();
        }
    }
}
