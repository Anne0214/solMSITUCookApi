using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Dtos.ResultModel
{
    public class NotificationRelatedMemberResultModel
    {
        /// <summary>
        /// 會員id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 會員頭貼
        /// </summary>
        public string ProfilePicture { get; set; }
        /// <summary>
        /// 會員暱稱
        /// </summary>
        public string Nickname { get; set; }
    }
}
