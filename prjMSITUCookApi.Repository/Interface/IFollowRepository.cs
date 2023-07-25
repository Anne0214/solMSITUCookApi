using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Dtos.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Interface
{
    public interface IFollowRepository
    {
        /// <summary>
        /// 查詢某用戶的追蹤/粉絲列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IEnumerable<FollowDataModel> GetList(FollowSearchCondition info);

        /// <summary>
        /// 查詢單筆追蹤關係的詳情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FollowDataModel Get(int id);

        /// <summary>
        /// 追蹤
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool Insert(FollowCondition info);

        /// <summary>
        /// 取消追蹤
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool Delete(FollowDeleteCondition info);
    }
}
