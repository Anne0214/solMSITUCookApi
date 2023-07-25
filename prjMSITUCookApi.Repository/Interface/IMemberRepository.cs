using prjMSITUCookApi.Repository.Dtos.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Interface
{
    public interface IMemberRepository
    {
        /// <summary>
        /// 取得用戶資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MemberDataModel Get(int id);
    }
}
