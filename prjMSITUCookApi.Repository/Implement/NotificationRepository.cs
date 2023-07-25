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
    public class NotificationRepository : INotificationRepository
    {

        private readonly string _connectString = @"Data Source=.;Initial Catalog=iSpanDataBaseUCook_V2;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true";


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

        IEnumerable<NotificationDataModel> INotificationRepository.GetList(NotificationSearchCondition info)
        {
            //todo 改sql
            var sql = @"Select * From NOTIFICATION_RECORD_通知紀錄
                            Where NOTIFICATION_RECORD_通知紀錄_PK = id";
            var parameter = new DynamicParameters();
            parameter.Add("Id", info.MemberId);
            parameter.Add("Type", info.Type);
            using (var conn = new SqlConnection())
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
