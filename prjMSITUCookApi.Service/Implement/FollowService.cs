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
        private readonly INotificationRepository _notificationRepo;
        private readonly IFollowRepository _followRepository;
        private readonly IMapper _mapper;

        public FollowService() {
            this._followRepository = new FollowRepository();
            this._notificationRepo = new NotificationRepository();
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ServiceMappings>());

            this._mapper = config.CreateMapper();
        }
        public bool Delete(FollowDeleteInfo info)
        {
            //找到相關通知(不增加dto參數，因為只適用這個情境)
            NotificationSearchCondition filter = new NotificationSearchCondition() { 
                MemberId = info.FollowWhom,
                Type =5
            };
            var notification = _notificationRepo.GetList(filter).Where(x=>x.LINKED_MEMBER_ID相關會員_FK==info.WhoFollow).FirstOrDefault();
            //如果在該用戶的通知表有東西，就刪掉該則通知
            if (notification != null) {
                _notificationRepo.Delete(notification.NOTIFICATION_RECORD_通知紀錄_PK);
            }
            //將info轉為condition
            var condition = this._mapper.Map<FollowDeleteInfo, FollowDeleteCondition>(info);
            //調用repo取得結果
            var data = this._followRepository.Delete(condition);


            
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
            //通知被通知的用戶 -> 被追蹤用戶的通知欄新增
            NotificationCondition input = new NotificationCondition() {
                MemberId = info.FollowWhom,//被追蹤用戶
                RelatedMemberId = info.WhoFollow,
                NotificationTime = DateTime.Now,
                ReadTime = null,
                RelatedOrderId = null,
                RelatedRecipeId = null,
                Type =5,
            };
            //創建通知
            var NotifyFollow=_notificationRepo.Create(input);
            //todo 針對創建通知成功與否的處理(用transation? 只因為發不了通知所以不能追蹤超怪，或留著有空再發?)

            //將info轉為condition
            var condition = this._mapper.Map<FollowInfo, FollowCondition>(info);
            //調用repo取得結果
            var result = this._followRepository.Insert(condition);
            //轉為最終result
            return result;
        }
    }
}
