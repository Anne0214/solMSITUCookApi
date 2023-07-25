using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Dtos.ResultModel
{
    /// <summary>
    /// 最終傳回主專案的東西
    /// </summary>
    public class FollowResultModel
    {
        /// <summary>
        /// 該條紀錄編號
        /// </summary>
        public int FollowId { get; set; }
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
