using Dapper;
using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Implement
{
    public class LikeRepository : ILikeRepository
    {
        private readonly string _connectString = @"Data Source=.;Initial Catalog=iSpanDataBaseUCook_V2;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true";

        bool ILikeRepository.Delete(LikeDeleteCondition info)
        {
            string sql = @"Delete From [LATEST_LIKE_LOG_最新按讚紀錄]
                            Where [MEMBER_ID會員_FK]=@Member
                            And [LIKED_RECIPE按讚食譜_FK]=@Recipe";

            var parameter = new DynamicParameters();
            parameter.Add("Member", info.MemberId);
            parameter.Add("Recipe", info.RecipeId);

            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Execute(sql, parameter);
                return result > 0;
            }
        }

        bool ILikeRepository.Post(LikeCondition info)
        {
            string sql = @"Insert Into [LATEST_LIKE_LOG_最新按讚紀錄]
                            Values(@Member,@Recipe,@Time)";
            var parameter = new DynamicParameters();
            parameter.Add("Member", info.MemberId);
            parameter.Add("Recipe", info.RecipeId);
            parameter.Add("time", DateTime.Now);

            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Execute(sql, parameter);
                return result > 0;
            }
        }
    }
}
