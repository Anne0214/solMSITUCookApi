using prjMSITUCookApi.Repository.Dtos.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Interface
{
    public interface IOrderRepository
    {
        /// <summary>
        /// 取得一則訂單資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDataModel Get(int id);
    }
}
