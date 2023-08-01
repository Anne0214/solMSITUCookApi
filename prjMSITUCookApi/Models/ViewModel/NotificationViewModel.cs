namespace prjMSITUCookApi.Models.ViewModel
{
    public class NotificationViewModel
    {
        /// <summary>
        /// 通知id
        /// </summary>
        public int NotificationId { get; set; }
        /// <summary>
        /// 收到通知的會員Id
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 收到通知的時間
        /// </summary>
        public DateTime NotificationTime { get; set; }
        /// <summary>
        /// 已讀時間
        /// </summary>
        public DateTime? ReadTime { get; set; }
        /// <summary>
        /// 通知類型id
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 通知類型名稱
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 該則通知相關食譜資訊
        /// </summary>
        public NotificationRelatedRecipeViewModel? RelatedRecipe { get; set; }
        /// <summary>
        /// 該則通知相關會員資訊
        /// </summary>
        public NotificationRelatedMemberViewModel? RelatedMember { get; set; }
        /// <summary>
        /// 該則通知相關訂單資訊
        /// </summary>
        public NotificationRelatedOrderViewModel? RelatedOrder { get; set; }
        /// <summary>
        /// for type=6，紀錄合併的訊息數
        /// </summary>
        public int MergedNotificationCount { get; set; }
        /// <summary>
        /// 合併的通知編號
        /// </summary>
        public List<int> MergeNotificationId { get; set; }

    }

}
