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

            //todo 刪除相關的讚的通知

            return result;
        }

        /// <summary>
        /// 新增讚
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool ILikeService.Post(LikeInfo info)
        {
            try
            {
                //取得食譜資訊
                var recipe = _recipeRepo.Get(info.RecipeId);

                //新增通知
                NotificationCondition newNotification = new NotificationCondition()
                {
                    MemberId = recipe.AUTHOR_作者,
                    ReadTime = null,
                    NotificationTime = DateTime.Now,
                    Type = 4,
                    RelatedRecipeId = recipe.RECIPE食譜_PK
                };
                var createNotificationResult = _notificationRepo.Create(newNotification);

                //按讚
                var condition = this._mapper.Map<LikeInfo, LikeCondition>(info);
                var result = _likeRepo.Post(condition);
                return result;
            }
            catch {
                //todo 刪除通知紀錄
                //todo 刪除讚的紀錄
                return false;
            }
            
        }
    }
}
