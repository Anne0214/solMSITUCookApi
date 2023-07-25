using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.Condition
{
    public class FollowSearchCondition
    {
        /// <summary>
        /// 目標用戶
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 要找目標用戶的哪種清單 1.粉絲 2.追蹤
        /// </summary>
        public int Type { get; set; }
    }
}
