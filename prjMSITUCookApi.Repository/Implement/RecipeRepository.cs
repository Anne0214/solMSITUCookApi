using Dapper;
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
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _connectString = @"Data Source=.;Initial Catalog=iSpanDataBaseUCook_V2;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true";

        RecipeDataModel IRecipeRepository.Get(int id)
        {
            string sql = @"Select * From [RECIPE_食譜] as a
                            InnerJoin [MEMBER_會員] as b
                            on a.[AUTHOR_作者] = b.[MEMBER_ID會員_PK]
                            Where [RECIPE食譜_PK] = @Id";
            var parameter = new DynamicParameters();
            parameter.Add("Id", id);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<RecipeDataModel>(sql, parameter);
                return result;
            }


        }
    }
}
