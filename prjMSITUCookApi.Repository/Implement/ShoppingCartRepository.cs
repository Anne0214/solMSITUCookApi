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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly string _connectString = @"Data Source=.;Initial Catalog=iSpanDataBaseUCook_V2;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true";

        public List<ShoppingCartDataModel> GetCartByMemberId(int memberId)
        {
            var sql = @"SELECT * FROM [dbo].[CART_購物車] WHERE [MEMBER_ID會員_FK] = @MEMBERID";
            var parameter = new DynamicParameters();
            parameter.Add("@MEMBERID", memberId);
            using var conn = new SqlConnection(_connectString);
            return conn.Query<ShoppingCartDataModel>(sql, parameter).ToList();
        }

        public bool Insert(ShoppingCartPostCondition model)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[CART_購物車]           
                        VALUES(@MEMBERID,@SPU,@QUANTITY,@SETUPTIME,@SKU)
                         SELECT @@IDENTITY";
                var parameter = new DynamicParameters();
                parameter.Add("MEMBERID", model.MemberId會員Fk);
                parameter.Add("SPU", model.Spu);
                parameter.Add("QUANTITY", model.Quantity數量);
                parameter.Add("SETUPTIME", model.SetupTime建立時間);
                parameter.Add("SKU", model.Sku);

                using var conn = new SqlConnection(_connectString);
                return conn.Execute(sql, parameter) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
