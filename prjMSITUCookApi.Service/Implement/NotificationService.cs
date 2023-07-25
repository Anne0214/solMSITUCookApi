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
    public class NotificationService : INotificationService
    {
        private readonly IRecipeRepository _recipeRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly INotificationRepository _notificationRepo;
        private readonly IMapper _mapper;
        public NotificationService() {
            _recipeRepo = new RecipeRepository();
            _orderRepo = new OrderRepository();
            _memberRepo = new MemberRepository();
            _notificationRepo = new NotificationRepository();

            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ServiceMappings>());

            this._mapper = config.CreateMapper();
        }

        bool INotificationService.Delete(int id)
        {
            var result = _notificationRepo.Delete(id);
            return result;
        }

        NotificationResultModel INotificationService.Get(int id)
        {
            var notification = _notificationRepo.Get(id);
            NotificationResultModel result = this._mapper.Map<NotificationDataModel, NotificationResultModel>(notification);
            if (notification.LINKED_MEMBER_ID相關會員_FK != null)
            {
                var member = _memberRepo.Get((int)notification.LINKED_MEMBER_ID相關會員_FK);
                NotificationRelatedMemberResultModel result_member = this._mapper.Map<MemberDataModel, NotificationRelatedMemberResultModel>(member);
                result.RelatedMember = result_member;

            }
            if (notification.LINKED_ORDER_ID相關訂單編號_FK != null)
            {
                var order = _orderRepo.Get((int)notification.LINKED_ORDER_ID相關訂單編號_FK);
                NotificationRelatedOrderResultModel result_order = this._mapper.Map<OrderDataModel, NotificationRelatedOrderResultModel>(order);
                result.RelatedOrder = result_order;
            }
            if (notification.LINKED_RECIPE相關食譜_FK != null)
            {
                var recipe = _recipeRepo.Get((int)notification.LINKED_RECIPE相關食譜_FK);
                NotificationRelatedRecipeResultModel result_recipe = this._mapper.Map<RecipeDataModel, NotificationRelatedRecipeResultModel>(recipe);
                result.RelatedRecipe = result_recipe;
            }
            
            
            return result;
        }

        IEnumerable<NotificationResultModel> INotificationService.GetList(NotificationSearchInfo info)
        {
            //todo IEnumerable跟list
            var condition = this._mapper.Map<NotificationSearchInfo,NotificationSearchCondition>(info);
            var list = _notificationRepo.GetList(condition);
            List<NotificationResultModel> result = new List<NotificationResultModel>();  
            foreach (var i in list) {
                NotificationResultModel item = this._mapper.Map<NotificationDataModel, NotificationResultModel>(i);

                if (i.LINKED_MEMBER_ID相關會員_FK != null)
                {
                    var member = _memberRepo.Get((int)i.LINKED_MEMBER_ID相關會員_FK);
                    NotificationRelatedMemberResultModel result_member = this._mapper.Map<MemberDataModel, NotificationRelatedMemberResultModel>(member);
                    item.RelatedMember = result_member;

                }
                if (i.LINKED_ORDER_ID相關訂單編號_FK != null)
                {
                    var order = _orderRepo.Get((int)i.LINKED_ORDER_ID相關訂單編號_FK);
                    NotificationRelatedOrderResultModel result_order = this._mapper.Map<OrderDataModel, NotificationRelatedOrderResultModel>(order);
                    item.RelatedOrder = result_order;
                }
                if (i.LINKED_RECIPE相關食譜_FK != null)
                {
                    var recipe = _recipeRepo.Get((int)i.LINKED_RECIPE相關食譜_FK);
                    NotificationRelatedRecipeResultModel result_recipe = this._mapper.Map<RecipeDataModel, NotificationRelatedRecipeResultModel>(recipe);
                    item.RelatedRecipe = result_recipe;
                }
                result.Add(item);

            }
            return result;
        }
        
        bool INotificationService.Read(int id)
        {
            var result = _notificationRepo.Read(id);
            return result;
        }

        bool INotificationService.ReadList(List<int> idList)
        {
            throw new NotImplementedException();
        }
    }
}
