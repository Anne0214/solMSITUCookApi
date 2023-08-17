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
            //找到該筆讚的紀錄
            LikeSearchCondition like = new LikeSearchCondition() {
                MemberId = info.MemberId,
                RecipeId = info.RecipeId,
            };
            if (like == null) {
                return false;
            }
            try { 
                //如果時間很近就要刪除通知，很遠就放著
                var date = _likeRepo.GetList(like).FirstOrDefault().LIKED_TIME按讚時間;
                var authorId = _recipeRepo.Get(info.RecipeId).AUTHOR_作者;
                if ((DateTime.Now - date).TotalDays < 30) {
                    //刪除讚的通知
                    var target = new NotificationSearchCondition()
                    {
                        MemberId = authorId, //換成作者
                        Type = 4
                    };
                    //取離
                    var notificationId = _notificationRepo
                                            .GetList(target)
                                            .OrderBy(x=>Math.Abs((x.NOTIFY_TIME通知時間-date).TotalMilliseconds)) //時間相減的絕對值，從小排到大
                                            .FirstOrDefault() //取最小的
                                            .NOTIFICATION_RECORD_通知紀錄_PK;
                    var deleteNotification = _notificationRepo.DeleteById(notificationId);
                }
            }
            catch {
                //todo log
            }
            try
            {
                //刪除讚的紀錄
                var condition = this._mapper.Map<LikeDeleteInfo, LikeDeleteCondition>(info);
                var result = _likeRepo.Delete(condition);

                return result;
            }
            catch {
                //todo log
                return false;
            }
        }

        /// <summary>
        /// 新增讚
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool ILikeService.Post(LikeInfo info)
        {
            //取得食譜資訊
            var recipe = _recipeRepo.Get(info.RecipeId);

            //確認食譜存在
            if (recipe == null) {
                return false;
            }

            //確認是否已經按讚過，已按讚過不得重複按讚
            LikeSearchCondition like = new LikeSearchCondition()
            {
                MemberId = info.MemberId,
                RecipeId = info.RecipeId,
            };
            if (_likeRepo.GetList(like).FirstOrDefault() != null) {
                return false;
            }
            var timeStamp = DateTime.Now;

            try{
                //新增通知
                NotificationCondition newNotification = new NotificationCondition()
                {
                    MemberId = recipe.AUTHOR_作者,
                    ReadTime = null,
                    NotificationTime = timeStamp,
                    Type = 4,
                    RelatedRecipeId = recipe.RECIPE食譜_PK
                };
                var createNotificationResult = _notificationRepo.Create(newNotification);
            }
            catch { 
                //todo log紀錄
            }

            try {
                //新增按讚
                info.Time = timeStamp;
                var condition = this._mapper.Map<LikeInfo, LikeCondition>(info);
                var result = _likeRepo.Post(condition);
                return result;
            } catch{ 
                //刪除通知紀錄
                var target = new NotificationSearchCondition()
                {
                    MemberId = recipe.AUTHOR_作者,
                    Type = 4
                };
                var id = _notificationRepo.GetList(target).FirstOrDefault(x=>x.NOTIFY_TIME通知時間== timeStamp).NOTIFICATION_RECORD_通知紀錄_PK;
                bool deleteNotification = _notificationRepo.DeleteById(id);

                return false;
            }
        }
    }
}

