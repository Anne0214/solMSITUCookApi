using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class MemberDataModel
    {
       public int MEMBER_ID會員_PK { get; set; }
       public string NICK_NAME暱稱 { get; set; }
       public string PROFILE_PHOTO頭貼 { get; set; }
    }
}
