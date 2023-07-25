using AutoMapper;
using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Implement;
using prjMSITUCookApi.Repository.Interface;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Interface;
using prjMSITUCookApi.Service.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Implement
{
    public class LikeService : ILikeService
    {

        private readonly ILikeRepository _likeRepo;
        private readonly IMapper _mapper;

        public LikeService() {
            _likeRepo = new LikeRepository();
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ServiceMappings>());

            this._mapper = config.CreateMapper();
        }

        bool ILikeService.Delete(LikeDeleteInfo info)
        {
            var condition = this._mapper.Map<LikeDeleteInfo, LikeDeleteCondition>(info);
            var result = _likeRepo.Delete(condition);

            return result;
        }

        bool ILikeService.Post(LikeInfo info)
        {
            var condition = this._mapper.Map<LikeInfo, LikeCondition>(info);
            var result = _likeRepo.Post(condition);

            return result;
        }
    }
}
