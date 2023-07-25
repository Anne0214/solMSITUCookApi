using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class OrderDataModel
    {
      public int ORDER_NUMBER訂單號碼_PK { get; set; }
      public int MEMBER_ID會員_FK { get; set; }
      public string img { get; set; }

    }
}
