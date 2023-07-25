using AutoMapper;
using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Dtos.DataModel;
using prjMSITUCookApi.Repository.Implement;
using prjMSITUCookApi.Repository.Interface;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;
using prjMSITUCookApi.Service.Interface;
using prjMSITUCookApi.Service.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Implement
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IMapper _mapper;

        public FollowService() {
            this._followRepository = new FollowRepository();
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ServiceMappings>());

            this._mapper = config.CreateMapper();
        }
        public bool Delete(FollowDeleteInfo info)
        {   
            //將info轉為condition
            var condition = this._mapper.Map<FollowDeleteInfo, FollowDeleteCondition>(info);
            //調用repo取得結果
            var data = this._followRepository.Delete(condition);
            //轉為最終result
            return data;
        }

        public FollowResultModel Get(int id)
        {
            //調用repo取得結果
            var data = this._followRepository.Get(id);
            //轉為最終result
            var result = this._mapper.Map<FollowDataModel, FollowResultModel>(data);
            return result;
        }

        public IEnumerable<FollowResultModel> GetList(FollowSearchInfo info)
        {
            //將info轉為condition
            var condition = this._mapper.Map<FollowSearchInfo, FollowSearchCondition>(info);
            //調用repo取得結果
            var data = this._followRepository.GetList(condition);
            //轉為最終result
            var result = this._mapper.Map<IEnumerable<FollowDataModel>, IEnumerable<FollowResultModel>>(data);
            return result;
        }

        public bool Insert(FollowInfo info)
        {
            //將info轉為condition
            var condition = this._mapper.Map<FollowInfo, FollowCondition>(info);
            //調用repo取得結果
            var result = this._followRepository.Insert(condition);
            //轉為最終result
            return result;
        }
    }
}
