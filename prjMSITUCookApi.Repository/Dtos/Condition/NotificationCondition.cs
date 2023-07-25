using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.Condition
{
    public class NotificationCondition
    {
        public int MemberId { get; set; }
        public DateTime NotificationTime { get; set; }
        public DateTime? ReadTime { get; set; }
        public int Type { get; set; }
        public int? RelatedRecipeId { get; set; }
        public int? RelatedMemberId { get; set; }
        public int? RelatedOrderId { get; set; }

    }
}
