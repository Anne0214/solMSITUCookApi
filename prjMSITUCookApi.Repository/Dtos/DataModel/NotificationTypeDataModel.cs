using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class NotificationTypeDataModel
    {
        public int NOTIFICATION_TYPE_通知類型編號_PK { get; set; }
        public string NOTIFICATION_TYPE_NAME通知類型名稱 { get; set; }
        public string NOTIFICATION_TRIGGER通知時機 { get; set; }
    }
}
