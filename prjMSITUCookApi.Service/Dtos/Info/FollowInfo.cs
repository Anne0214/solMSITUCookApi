using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Dtos.Info
{
    /// <summary>
    /// 建立所需資料
    /// </summary>
    public class FollowInfo
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
