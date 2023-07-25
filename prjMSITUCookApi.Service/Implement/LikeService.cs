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
        private readonly IRecipeRepository _recipeRepo;
        private readonly INotificationRepository _notificationRepo;
        private readonly IMapper _mapper;

        public LikeService() {
            _likeRepo = new LikeRepository();
            _recipeRepo = new RecipeRepository();
            _notificationRepo = new NotificationRepository();
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ServiceMappings>());

            this._mapper = config.CreateMapper();
        }

        /// <summary>
        /// 取消按讚
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool ILikeService.Delete(LikeDeleteInfo info)
        {
            //刪除讚的紀錄
            var condition = this._mapper.Map<LikeDeleteInfo, LikeDeleteCondition>(info);
            var result = _likeRepo.Delete(condition);

            return result;
        }

        /// <summary>
        /// 新增讚
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool ILikeService.Post(LikeInfo info)
        {
            //todo 非同步，以及如果其中一邊失敗怎麼辦
            //取得食譜資訊
            var recipe = _recipeRepo.Get(info.RecipeId);
            
            //新增通知
            NotificationCondition newNotification = new NotificationCondition() { 
                MemberId = recipe.AUTHOR_作者,
                ReadTime =null,
                NotificationTime=DateTime.Now,
                Type = 4,
                RelatedRecipeId = recipe.RECIPE食譜_PK
            };
            var createNotificationResult = _notificationRepo.Create(newNotification);

            //刪除之前的合併
            //if (createNotificationResult) {
            //    //取得要刪除的通知
            //    var oldNotification = _notificationRepo.GetList(new NotificationSearchCondition() { 
            //        MemberId = recipe.AUTHOR_作者,
            //        Type =4
            //    }).Where(x=>x.LINKED_RECIPE相關食譜_FK==recipe.RECIPE食譜_PK).FirstOrDefault();
            //    //刪除
            //    var deleteResult = _notificationRepo.Delete(oldNotification.NOTIFICATION_RECORD_通知紀錄_PK);
            //}
            //按讚
            var condition = this._mapper.Map<LikeInfo, LikeCondition>(info);
            var result = _likeRepo.Post(condition);

            return result;
        }
    }
}
