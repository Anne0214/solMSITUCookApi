using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Interface
{
    public interface INotificationService
    {
        //取得一筆通知
        NotificationResultModel Get(int id);

        //取得某用戶的某類通知(或全部)
        IEnumerable<NotificationResultModel> GetList(NotificationSearchInfo info);

        //針對一則通知改為已讀
        bool Read(int id);

        //針對一群通知改為已讀
        bool ReadList(List<int> idList);

        //刪除一則通知
        bool DeleteById(int id);
    }
}
