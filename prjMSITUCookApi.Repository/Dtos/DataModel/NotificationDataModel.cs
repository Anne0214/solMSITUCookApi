using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class NotificationDataModel
    {
       public int NOTIFICATION_RECORD_通知紀錄_PK { get; set; }
       public int MEMBER_ID會員_FK { get; set; }
       public DateTime NOTIFY_TIME通知時間 { get; set; }
       public DateTime? READED_已讀時間 { get; set; }
       public int NOTIFICATION_TYPE通知類型編號 { get; set; }
       public int? LINKED_RECIPE相關食譜_FK { get; set; }
       public int? LINKED_MEMBER_ID相關會員_FK { get; set; }
       public int? LINKED_ORDER_ID相關訂單編號_FK { get; set; }
    }
}
