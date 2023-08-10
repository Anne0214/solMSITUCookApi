using System.ComponentModel.DataAnnotations;

namespace prjMSITUCookApi.Models.Parameter
{
    public class FollowParameter
    {
        /// <summary>
        /// 誰追蹤
        /// </summary>
        [Required]
        public int WhoFollow { get; set; }
        /// <summary>
        /// 追蹤誰
        /// </summary>
        [Required]
        public int FollowWhom { get; set; }

    }
}
