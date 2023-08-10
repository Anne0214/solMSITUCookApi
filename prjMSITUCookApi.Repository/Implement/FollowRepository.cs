using Dapper;
using prjMSITUCookApi.Common;
using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Dtos.DataModel;
using prjMSITUCookApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Implement
{
    public class FollowRepository : IFollowRepository
    {
        private readonly string _connectString = @"Data Source=.;Initial Catalog=iSpanDataBaseUCook_V2;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true";


        /// <summary>
        /// 取得單筆資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FollowDataModel IFollowRepository.Get(int id)
        {
            var sql = @"Select * From FOLLOW_追蹤 Where [FOLLOW_追蹤_ID] = @Id";
            var parameter = new DynamicParameters();
            parameter.Add("Id", id);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<FollowDataModel>(sql, parameter);
                return result;
            }
        }
        /// <summary>
        /// 取得某人的追蹤清單
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IEnumerable<FollowDataModel> IFollowRepository.GetList(FollowSearchCondition info)
        {
            string sql = null;
            if (info.Type == Dictionary.GetFollowListTypeNumber)
            {
                sql = @"Select * From FOLLOW_追蹤 Where [FOLLOWER_MEMBER_ID追蹤者_FK]=@Id";
            }
            else if (info.Type == Dictionary.GetFanListTypeNumber)
            {
                sql = @"Select * From FOLLOW_追蹤 Where [FOLLOWED_MEMBER_ID被追蹤者_FK]=@Id";
            }
            else {
                return null;
            }
            var parameter = new DynamicParameters();
            parameter.Add("Id",info.MemberId);
            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Query<FollowDataModel>(sql,parameter);
                return result;
            }
            
        
        }

        bool IFollowRepository.Insert(FollowCondition info)
        {
            var sql = @"Insert Into FOLLOW_追蹤
                         Values(@WhoFollow,@FollowWhom,@Time)
                         SELECT @@IDENTITY";
            var parameter = new DynamicParameters();
            parameter.Add("WhoFollow",info.WhoFollow);
            parameter.Add("FollowWhom", info.FollowWhom);
            parameter.Add("Time", info.FollowTime);
            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Execute(sql, parameter);

                return result > 0;
            }
        }
        bool IFollowRepository.Delete(FollowDeleteCondition info)
        {
            var sql = @"Delete From FOLLOW_追蹤 
                        Where [FOLLOWER_MEMBER_ID追蹤者_FK]=@WhoFollow and 
                                [FOLLOWED_MEMBER_ID被追蹤者_FK] = @FollowWhom";
            var parameter = new DynamicParameters();
            parameter.Add("WhoFollow",info.WhoFollow);
            parameter.Add("FollowWhom", info.FollowWhom);

            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Execute(sql, parameter); //傳回影響筆數
                return result > 0;
            }

        }
    }
}
