﻿namespace prjMSITUCookApi.Models.Parameter
{
    public class LikeDeleteParameter
    {
        /// <summary>
        /// 對該文章按讚的用戶
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 按讚的食譜Id
        /// </summary>
        public int RecipeId { get; set; }
    }
}
