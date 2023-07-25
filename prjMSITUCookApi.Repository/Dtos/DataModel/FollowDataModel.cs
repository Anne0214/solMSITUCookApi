using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class FollowDataModel
    {
        public int FOLLOW_追蹤_ID { get; set; }
        public int FOLLOWER_MEMBER_ID追蹤者_FK { get; set; }
        public int FOLLOWED_MEMBER_ID被追蹤者_FK { get; set; }
        public DateTime FOLLOW_TIME開始追蹤日期 { get; set; }
    }
}
