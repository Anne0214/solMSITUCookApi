using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Dtos.Info
{
    public class NotificationSearchInfo
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
