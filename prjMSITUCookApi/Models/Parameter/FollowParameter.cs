﻿namespace prjMSITUCookApi.Models.Parameter
{
    public class FollowParameter
    {
        /// <summary>
        /// 誰追蹤
        /// </summary>
        public int WhoFollow { get; set; }
        /// <summary>
        /// 追蹤誰
        /// </summary>
        public int FollowWhom { get; set; }
        /// <summary>
        /// 追蹤發生時間
        /// </summary>
        public DateTime FollowTime { get; set; }
    }
}
