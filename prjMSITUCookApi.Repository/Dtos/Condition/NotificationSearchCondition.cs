using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.Condition
{
    public class NotificationSearchCondition
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
