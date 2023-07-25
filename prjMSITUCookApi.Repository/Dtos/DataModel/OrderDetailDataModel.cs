using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class OrderDetailDataModel
    {
     public int ORDER_DETAIL_ID訂單明細編號 { get; set; }
     public int ORDER_NUMBER訂單號碼_FK { get; set; }
     public int SPU { get; set; }

    }
}
