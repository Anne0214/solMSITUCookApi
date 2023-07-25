using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Dtos.ResultModel
{
    public class NotificationRelatedRecipeResultModel
    {
        /// <summary>
        /// 作者的會員id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 食譜名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 食譜封面
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 作者暱稱
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// 作者頭貼
        /// </summary>
        public string AuthorProfilePicture { get; set; }
        /// <summary>
        /// 食譜讚數
        /// </summary>
        public int Likes { get; set; }
        /// <summary>
        /// 食譜發文時間
        /// </summary>
        public DateTime PublishedTime { get; set; }

    }
}
