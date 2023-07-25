﻿using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Dtos.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Interface
{
    public interface INotificationRepository
    {
        //取得通知
        NotificationDataModel Get(int id);

        //取得某用戶的某類通知(或全部)
        IEnumerable<NotificationDataModel> GetList(NotificationSearchCondition info);
        
        //針對一則通知改為已讀
        bool Read(int id);

        //針對一群通知改為已讀
        bool ReadList(List<int> idList);

        //刪除一則通知
        bool Delete(int id);
    }
}
