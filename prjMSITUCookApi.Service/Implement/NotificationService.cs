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
using System.Collections;
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
            _likeRepo = new LikeRepository();
            _typeRepo = new NotificationTypeRepository();

            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ServiceMappings>());

            this._mapper = config.CreateMapper();
        }

        bool INotificationService.DeleteById(int id)
        {
            try
            {
                var result = _notificationRepo.DeleteById(id);
                return result;
            }
            catch {
                return false;
            }
        }

        NotificationResultModel INotificationService.Get(int id)
        {
            var notification = _notificationRepo.GetById(id);
            if (notification == null) {
                return null;
            }

            try { 
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
            catch { 
                return null; 
            }
            
        }

        IEnumerable<NotificationResultModel> INotificationService.GetList(NotificationSearchInfo info)
        {
            //todo IEnumerable跟list
            var condition = this._mapper.Map<NotificationSearchInfo,NotificationSearchCondition>(info);
            var list = _notificationRepo.GetList(condition);

            if (list == null || list.Count() <= 0) {
                return null;
            }

            List<NotificationResultModel> result = new List<NotificationResultModel>();
            try { 
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
                        var target = new LikeSearchCondition()
                        {
                            RecipeId = recipe.RECIPE食譜_PK
                        };
                        var likeList = _likeRepo.GetList(target);
                        if (likeList != null && likeList.Count() > 0) {
                            var likes = likeList.Count();
                            result_recipe.Likes += likes;
                        }
                        item.RelatedRecipe = result_recipe;
                    }
                    var typeName = _typeRepo.Get(i.NOTIFICATION_TYPE通知類型編號).NOTIFICATION_TYPE_NAME通知類型名稱;
                    item.TypeName = typeName;
                    result.Add(item);
                }
            }
            catch {
                return null;
            }

            try { 
                //近一周的追蹤通知合成一條
                //取得近一周通知且type=5的通知
                var delete = result.Where(x => (DateTime.Now.AddDays(-7) - x.NotificationTime).TotalDays < 0
                                && x.Type == 5).ToList();
                //確認裡面有沒有未讀的訊息，只要有一條，該合併後的訊息就要顯示未讀
                bool unreadExist = delete.Any(x=>x.ReadTime==null);
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
                        ReadTime = unreadExist?null:DateTime.Now, //合併的訊息中有任何一條是未讀則要顯示未讀
                        MergedNotificationCount = delete.Count(),
                        RelatedMember = delete.ToList()[0].RelatedMember,//隨機挑選幸運觀眾
                        MergeNotificationId = delete.Select(x=>x.NotificationId).ToList() //合併的訊息們的編號
                    };
                    result.Add(mergeNotification);
                }
            }
            catch {
                return result;
            }

            try {
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
                //從result裡面丟掉，並排序最後結果
                result = result.Except(trash)
                               .OrderByDescending(x => x.NotificationTime)
                               .ToList();
                return result;
            }
            catch {
                return result;
            }
            
        }
        
        bool INotificationService.Read(int id)
        {
            try
            {
                //如果該通知type=4，會找同recipe的之前的通知通通已讀
                var notification = _notificationRepo.GetById(id);
                var condition = new NotificationSearchCondition()
                {
                    MemberId = notification.MEMBER_ID會員_FK,
                    Type = 4
                };
                //找到過去的、同食譜的、未讀的通知們
                var mustReadList = _notificationRepo.GetList(condition)
                                .Where(x => x.LINKED_RECIPE相關食譜_FK == notification.LINKED_RECIPE相關食譜_FK &&
                                        x.NOTIFY_TIME通知時間 <= notification.NOTIFY_TIME通知時間
                                        && x.READED_已讀時間 == null)
                                .Select(y => y.NOTIFICATION_RECORD_通知紀錄_PK)
                                .ToList();
                //全部弄成已讀
                var result = _notificationRepo.ReadList(mustReadList);

                return result;
            }
            catch {
                return false;
            }
        }

        bool INotificationService.ReadList(List<int> idList)
        {
            try { 
            //要丟給已讀的清單
            List<int> mustReadList = new List<int>();

            //清理idList的重複
            idList = idList.Distinct().ToList();

            //清除0
            idList.Remove(0);
            
            //加入目前要已讀的編號們
            mustReadList.AddRange(idList);

            //商業邏輯: type4號通知本身有篩選，GetList只顯示同食譜編號的最新通知
            //故需要把readlist裡面的四號通知，找出在他之前尚未已讀的同食譜編號通知
            //一起做已讀

            foreach (int i in idList) {
                //找出前端裡面要已讀的四號通知
                var notification=_notificationRepo.GetById(i);
                if (notification != null)
                {
                    if (notification.NOTIFICATION_TYPE通知類型編號 == 4)
                    {
                        //找出該用戶的所有四號通知
                        var condition = new NotificationSearchCondition()
                        {
                            MemberId = notification.MEMBER_ID會員_FK,
                            Type = 4
                        };
                        //再篩選出該四號同知的同食譜、舊通知、尚未已讀的通知，取得他們的通知pk
                        var mustRead = _notificationRepo.GetList(condition)
                                    .Where(x => x.LINKED_RECIPE相關食譜_FK == notification.LINKED_RECIPE相關食譜_FK
                                            && x.NOTIFY_TIME通知時間 <= notification.NOTIFY_TIME通知時間
                                            && x.READED_已讀時間 == null)
                                    .Select(y => y.NOTIFICATION_RECORD_通知紀錄_PK)
                                    .ToList();
                        //加入mustReadList清單
                        mustReadList.AddRange(mustRead);
                    }
                }
            }
            //清理重複
            mustReadList = mustReadList.Distinct().ToList();

            //清除0
            mustReadList.Remove(0);

            var result = _notificationRepo.ReadList(mustReadList);
            return result;
            }
            catch {
                return false;
            }
        }
    }
}
