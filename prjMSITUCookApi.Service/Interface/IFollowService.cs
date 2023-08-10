using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Interface
{
    /// <summary>
    /// 追蹤的服務介面
    /// </summary>
    public interface IFollowService
    {
        /// <summary>
        /// 查詢某用戶的追蹤/粉絲列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IEnumerable<FollowResultModel> GetList(FollowSearchInfo info);

        /// <summary>
        /// 查詢單筆追蹤關係的詳情
        /// </summary>
        /// <param name="id">追蹤關係在表中的PK</param>
        /// <returns></returns>
        FollowResultModel Get(int id);

        /// <summary>
        /// 新增追蹤
        /// </summary>
        /// <param name="info">建立追蹤所需資料</param>
        /// <returns></returns>
        string Insert(FollowInfo info);

        /// <summary>
        /// 取消追蹤
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool Delete(FollowDeleteInfo info);

    }
}
