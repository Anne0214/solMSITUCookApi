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
using System.Security.Cryptography.X509Certificates;
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
        private readonly ILikeRepository _likeRepo;
        private readonly INotificationTypeRepository _typeRepo;
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
                var likes = _likeRepo.GetList(new LikeSearchCondition()
                {
                    RecipeId = recipe.RECIPE食譜_PK
                }).Count();
                result_recipe.Likes += likes;
                result.RelatedRecipe = result_recipe;
            }
            var typeName = _typeRepo.Get(notification.NOTIFICATION_TYPE通知類型編號).NOTIFICATION_TYPE_NAME通知類型名稱;
            result.TypeName = typeName;
            
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
                    var likes = _likeRepo.GetList(new LikeSearchCondition()
                    {
                        RecipeId = recipe.RECIPE食譜_PK
                    }).Count();
                    result_recipe.Likes += likes;
                    item.RelatedRecipe = result_recipe;
                }
                var typeName = _typeRepo.Get(i.NOTIFICATION_TYPE通知類型編號).NOTIFICATION_TYPE_NAME通知類型名稱;
                item.TypeName = typeName;
                result.Add(item);

            }
            //近一周的追蹤通知合成一條
            //取得近一周通知且type=5的通知
            var delete = result.Where(x => (DateTime.Now.AddDays(-7) - x.NotificationTime).TotalDays < 0
                            && x.Type == 5).ToList();
            //從result中刪除這些通知
            if (delete.Count() > 3) {
                result = result.Except(delete).ToList();
                //撰寫新的放入result
                var mergeNotification = new NotificationResultModel()
                {
                    NotificationTime = DateTime.Now,
                    MemberId = info.MemberId,
                    NotificationId = 0,
                    Type = 6,
                    TypeName = _typeRepo.Get(6).NOTIFICATION_TYPE_NAME通知類型名稱,
                    ReadTime = null,
                    count = delete.Count(),
                    RelatedMember = delete.ToList()[0].RelatedMember//隨機挑選幸運觀眾
                };
                result.Add(mergeNotification);
            }
            

            //我只給最新的一條type=4 && 同一個recipe
            //也就是在type=4的裡面，我只留薪的，刪舊的

            if (info.Type !=4 && info.Type !=0)
            {
                return result;
            }

            //先篩出所有type=4
            var filterType4 = result.Where(x => x.Type == 4).ToList();
            //挑出type=4裡面要留的
            var keep = filterType4.OrderByDescending(y => y.NotificationTime)
                .GroupBy(t => t.RelatedRecipe.Id)
                .Select(x => x.FirstOrDefault());
            //挑出type=4裡面要丟的
            var trash = filterType4.Except(keep);
            //從result裡面丟掉
            result = result.Except(trash).ToList();

            
            return result;
        }
        
        bool INotificationService.Read(int id)
        {
            var result = _notificationRepo.Read(id);
            return result;
        }

        bool INotificationService.ReadList(List<int> idList)
        {
            var result = _notificationRepo.ReadList(idList);
            return result;
        }
    }
}
