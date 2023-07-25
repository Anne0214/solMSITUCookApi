using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Dtos.ResultModel
{
    public class NotificationRelatedOrderResultModel
    {
        /// <summary>
        /// 訂單id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 該訂單任一筆商品的圖片
        /// </summary>
        public string ProductPicture { get; set; }
        /// <summary>
        /// 會員
        /// </summary>
        public int MemberId { get; set; }

    }
}
