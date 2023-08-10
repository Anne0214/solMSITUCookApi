using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.Condition
{
    /// <summary>
    /// 建立所需資料
    /// </summary>
    public class FollowCondition
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
            /// 追蹤時間
            /// </summary>
            public DateTime FollowTime { get; set; }

    }
    }

