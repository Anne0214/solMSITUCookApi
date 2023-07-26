using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class LikeDataModel
    {
      public int LATEST_LIKE_LOG_最新按讚紀錄_PK { get; set; }
      public int MEMBER_ID會員_FK { get; set; }
      public int LIKED_RECIPE按讚食譜_FK { get; set; }
      public DateTime LIKED_TIME按讚時間 { get; set; }
    }
}
