using System.ComponentModel.DataAnnotations;

namespace prjMSITUCookApi.Models.Parameter
{
    public class FollowSearchParameter
    {
        /// <summary>
        /// 目標用戶
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 要找目標用戶的哪種清單 1.粉絲 2.追蹤
        /// </summary>
        [Range(1, 2)]
        public int Type { get; set; }
    }
}
