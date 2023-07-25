using prjMSITUCookApi.Repository.Dtos.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Interface
{
    public interface ILikeRepository
    {

        /// <summary>
        /// 點讚
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool Post(LikeCondition info);

        /// <summary>
        /// 取消點讚
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool Delete(LikeDeleteCondition info);

    }
}
