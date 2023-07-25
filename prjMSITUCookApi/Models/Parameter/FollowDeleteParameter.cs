namespace prjMSITUCookApi.Models.Parameter
{
    public class FollowDeleteParameter
    {
        /// <summary>
        /// 誰追蹤
        /// </summary>
        public int WhoFollow { get; set; }
        /// <summary>
        /// 追蹤誰
        /// </summary>
        public int FollowWhom { get; set; }
    }
}
