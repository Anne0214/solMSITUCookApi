using Dapper;
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

        IEnumerable<LikeDataModel> ILikeRepository.GetList(LikeSearchCondition info)
        {
            if (info.MemberId == 0 && info.RecipeId == 0) {
                return null;
            }
            string sql = @"Select * From [LATEST_LIKE_LOG_最新按讚紀錄] Where ";
            var parameter = new DynamicParameters();
            if (info.MemberId != 0) {
                sql += " [MEMBER_ID會員_FK]=@MemberId And";
                parameter.Add("MemberId", info.MemberId);
            }
            if (info.RecipeId != 0) {
                sql += " [LIKED_RECIPE按讚食譜_FK]=@RecipeId And";
                parameter.Add("RecipeId", info.RecipeId);
            }

            sql = sql.Substring(0, sql.Length - 3);

            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Query<LikeDataModel>(sql, parameter);
                return result;
            }
        }

        bool ILikeRepository.Post(LikeCondition info)
        {
            string sql = @"Insert Into [LATEST_LIKE_LOG_最新按讚紀錄]
                            Values(@Member,@Recipe,@Time)";
            var parameter = new DynamicParameters();
            parameter.Add("Member", info.MemberId);
            parameter.Add("Recipe", info.RecipeId);
            parameter.Add("time", info.Time);

            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Execute(sql, parameter);
                return result > 0;
            }
        }

        
    }
}
