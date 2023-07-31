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
    public class NotificationTypeRepository : INotificationTypeRepository
    {
        private readonly string _connectString = @"Data Source=.;Initial Catalog=iSpanDataBaseUCook_V2;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true";

        NotificationTypeDataModel INotificationTypeRepository.Get(int id)
        {
            string sql = @"Select *  From [NOTIFICATION_TYPE_通知類型] Where [NOTIFICATION_TYPE_通知類型編號_PK]=@Id";
            var parameter = new DynamicParameters();
            parameter.Add("Id", id);

            using (var conn = new SqlConnection(_connectString)) {
                var result = conn.QueryFirstOrDefault<NotificationTypeDataModel>(sql,parameter);
                return result;
            }
            
        }
    }
}
