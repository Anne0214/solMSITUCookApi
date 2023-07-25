namespace prjMSITUCookApi.Models.Parameter
{
    public class NotificationSearchParameter
    {
        /// <summary>
        /// 收到通知的用戶Id
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 通知類別號
        /// </summary>
        public int Type { get; set; }
    }
}
