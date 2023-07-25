using Dapper;
using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Dtos.DataModel;
using prjMSITUCookApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Implement
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly string _connectString = @"Data Source=.;Initial Catalog=iSpanDataBaseUCook_V2;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true";


        //刪除一則通知
        bool INotificationRepository.Delete(int id)
        {
            var sql = @"Delete From NOTIFICATION_RECORD_通知紀錄
                            Where NOTIFICATION_RECORD_通知紀錄_PK = @Id";
            var parameter = new DynamicParameters();
            parameter.Add("Id",id);
            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Execute(sql, parameter);
                return result > 0;
            }
        }

        //取得一則通知
        NotificationDataModel INotificationRepository.Get(int id)
        {
            var sql = @"Select * From NOTIFICATION_RECORD_通知紀錄
                            Where NOTIFICATION_RECORD_通知紀錄_PK = id";
            var parameter = new DynamicParameters();
            parameter.Add("Id", id);
            using (var conn = new SqlConnection()) {
                var result = conn.QueryFirstOrDefault<NotificationDataModel>(sql,parameter);
                return result;
            }
        }

        //新增通知
        public bool Create(NotificationCondition info) {
            var sql = @"Insert into NOTIFICATION_RECORD_通知紀錄
                            Values(@MemberId,@NotifyTime,null,@Type,@RelatedRecipe,@RelatedMember,@RelatedOrder)
                            SELECT @@IDENTITY";
            var parameter = new DynamicParameters();
            parameter.Add("MemberId", info.MemberId);
            parameter.Add("NotifyTime", DateTime.Now);
            parameter.Add("Type",info.Type);
            parameter.Add("RelatedMember",info.RelatedMemberId);
            parameter.Add("RelatedRecipe", info.RelatedRecipeId);
            parameter.Add("RelatedOrder", info.RelatedOrderId);
            using (var conn = new SqlConnection(_connectString)) { 
                var result = conn.Execute(sql, parameter);
                return result > 0;
            }
        }

        IEnumerable<NotificationDataModel> INotificationRepository.GetList(NotificationSearchCondition info)
        {

            var sql = @"Select * From NOTIFICATION_RECORD_通知紀錄
                            Where [MEMBER_ID會員_FK] = @Id
                            And [NOTIFICATION_TYPE通知類型編號]=@Type";
            var parameter = new DynamicParameters();
            parameter.Add("Id", info.MemberId);
            parameter.Add("Type", info.Type);
            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Query<NotificationDataModel>(sql, parameter);
                return result;
            }


        }

        bool INotificationRepository.Read(int id)
        {
            string sql = @"Update NOTIFICATION_RECORD_通知紀錄
                            Set READED_已讀時間=@DateTime
                            Where NOTIFICATION_RECORD_通知紀錄_PK = @Id";
            var parameter = new DynamicParameters();
            parameter.Add("DateTime", DateTime.Now);
            parameter.Add("Id", id);

            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.Execute(sql,parameter);
                return result > 0;
            }
        }

        bool INotificationRepository.ReadList(List<int> idList)
        {
            //todo 尚未實作
            foreach (int id in idList) {
                string sql = @"Update NOTIFICATION_RECORD_通知紀錄
                            Set READED_已讀時間=@DateTime
                            Where NOTIFICATION_RECORD_通知紀錄_PK = @Id";
                var parameter = new DynamicParameters();
                parameter.Add("DateTime", DateTime.Now);
                parameter.Add("Id", id);
            }
            return true;
        }
    }
}
